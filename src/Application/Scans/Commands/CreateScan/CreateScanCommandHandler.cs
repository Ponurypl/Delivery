using MediatR;
using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Scans.Commands.CreateScan;

public sealed class CreateScanCommandHandler : ICommandHandler<CreateScanCommand, ScanCreatedDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTime;
    private readonly IScanRepository _scanRepository;
    private readonly ITransportRepository _transportRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPublisher _publisher;
    private readonly IMapper _mapper;

    public CreateScanCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime, IScanRepository scanRepository,
                                    ITransportRepository transportRepository, IUserRepository userRepository, IMapper mapper, IPublisher publisher)
    {
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
        _transportRepository = transportRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _publisher = publisher;
    }

    public async Task<ErrorOr<ScanCreatedDto>> Handle(CreateScanCommand request, CancellationToken cancellationToken)
    {
        User? deliverer = await _userRepository.GetByIdAsync(new UserId(request.DelivererId), cancellationToken);
        if (deliverer is null)
        {
            return Failure.UserNotExists;
        }

        Transport? transport = await _transportRepository.GetByIdAsync(new TransportId(request.TransportId), cancellationToken);
        if (transport is null)
        {
            return Failure.TransportNotExists;
        }
        ErrorOr<Success> transportStatusCheck = transport.CheckIfScannable();
        if (transportStatusCheck.IsError)
        {
            return transportStatusCheck.Errors;
        }

        TransportUnit? transportUnit = transport.TransportUnits.FirstOrDefault(u => u.Id == new TransportUnitId(request.TransportUnitId));
        if (transportUnit is null)
        {
            return Failure.TransportUnitNotExists;
        }

        ErrorOr<Success> transportUnitStatusCheck = transportUnit.CheckIfScannable();
        if (transportUnitStatusCheck.IsError)
        {
            return transportUnitStatusCheck.Errors;
        }

        ErrorOr<Scan> scanCreateResult = Scan.Create(transportUnit.Id, deliverer.Id, _dateTime);
        if (scanCreateResult.IsError)
        {
            return scanCreateResult.Errors;
        }

        Scan scan = scanCreateResult.Value;

        List<Scan> existingScans = await _scanRepository.GetAllByTransportUnitIdAsync(transportUnit.Id, cancellationToken);


        if (transportUnit.MultiUnitDetails is not null)
        {
            if (request.Quantity is null)
            {
                return Failure.InvalidScanAmount;
            }
            
            double alreadyScannedAmount = Math.Round(existingScans.Sum(existingScan => existingScan.Quantity) ?? 0d, 3, MidpointRounding.AwayFromZero);
            double amountAvailableForScan = Math.Round(transportUnit.MultiUnitDetails.Amount - alreadyScannedAmount, 3, MidpointRounding.AwayFromZero);

            if (request.Quantity > amountAvailableForScan)
            {
                return Failure.InvalidScanAmount;
            }

            ErrorOr<Updated> result = scan.SetQuantity(request.Quantity.Value);
            if (result.IsError)
            {
                return result.Errors;
            }

            if (request.Quantity == amountAvailableForScan)
            {
                transportUnit.UpdateStatus(TransportUnitStatus.Delivered);
            }
            else if (transportUnit.Status is TransportUnitStatus.New)
            {
                transportUnit.UpdateStatus(TransportUnitStatus.PartiallyDelivered);
            }

        }
        else
        {
            if(existingScans.Any())
            {
                return Failure.ScanAlreadyExists;
            }

            transportUnit.UpdateStatus(TransportUnitStatus.Delivered);
        }

        if (request.Location is not null)
        {
            ErrorOr<Updated> result = scan.SetGeolocation(request.Location.Latitude, request.Location.Longitude,
                                             request.Location.Accuracy);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        _scanRepository.Add(scan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        ScanCreatedEvent eventToPublish = _mapper.Map<ScanCreatedEvent>(transport);
        eventToPublish.CreatedScan = _mapper.Map<CreatedScanDto>(scan);
        eventToPublish.ScannedTransportUnit = _mapper.Map<TransportUnitDto>(transportUnit);
        eventToPublish.EventDate = _dateTime.UtcNow;
        await _publisher.Publish(eventToPublish, cancellationToken);

        return new ScanCreatedDto { Id = scan.Id.Value };
    }
}
