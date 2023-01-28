using MediatR;
using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Application.Scans.Queries.GetTransportUnitScans;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.Entities;
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
    private readonly ISender _sender;

    public CreateScanCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime, IScanRepository scanRepository,
                                    ITransportRepository transportRepository, IUserRepository userRepository, ISender sender)
    {
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
        _transportRepository = transportRepository;
        _userRepository = userRepository;
        _sender = sender;
    }

    public async Task<ErrorOr<ScanCreatedDto>> Handle(CreateScanCommand request, CancellationToken cancellationToken)
    {
        TransportId transportId = new(request.TransportId);
        Transport? transport = await _transportRepository.GetByIdAsync(transportId, cancellationToken);
        if (transport is null)
        {
            return Failure.TransportNotExists;
        }

        TransportUnitId transportUnitId = new(request.TransportUnitId);
        TransportUnit? transportUnit = transport.TransportUnits.FirstOrDefault(u => u.Id == transportUnitId);
        if (transportUnit is null)
        {
            return Failure.TransportUnitNotExists;
        }

        UserId delivererId = new(request.DelivererId);
        User? deliverer = await _userRepository.GetByIdAsync(delivererId, cancellationToken);
        if (deliverer is null)
        {
            return Failure.UserNotExists;
        }
        ErrorOr<List<GetTransportUnitScansDto>> existingScans = await _sender.
                Send(new GetTransportUnitScansQuery { Id = transportUnit.Id.Value }, cancellationToken);

        ErrorOr<Scan> scanCreateResult = Scan.Create(transportUnitId, deliverer.Id, _dateTime);

        if (scanCreateResult.IsError)
        {
            return scanCreateResult.Errors;
        }

        Scan scan = scanCreateResult.Value;

        if (transportUnit.MultiUnitDetails is not null)
        {
            if (request.Quantity is null or <= 0)
            {
                return Failure.InvalidScanInput;
            }
            
            double alreadyScannedAmount = Math.Round(existingScans.Value.Sum(existingScan => existingScan.Quantity) ?? 0d, 3);
            double amountAvilableForScan = Math.Round(transportUnit.MultiUnitDetails.Amount - alreadyScannedAmount, 3);

            if (request.Quantity > amountAvilableForScan)
            {
                return Failure.ScanAbleAmountExceeded(amountAvilableForScan < 0 ? 0 : amountAvilableForScan);
            }



            ErrorOr<Updated> result = scan.AddQuantity(request.Quantity.Value);
            if (result.IsError)
            {
                return result.Errors;
            }
        }
        else
        {
            if(existingScans.Value is not null)
            {
                return Failure.ScanAbleAmountExceeded(0);
            }
        }

        if (request.Location is not null)
        {
            ErrorOr<Updated> result = scan.AddGeolocation(request.Location.Latitude, request.Location.Longitude,
                                             request.Location.Accuracy);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        _scanRepository.Add(scan);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return new ScanCreatedDto { Id = scan.Id.Value };
    }
}

