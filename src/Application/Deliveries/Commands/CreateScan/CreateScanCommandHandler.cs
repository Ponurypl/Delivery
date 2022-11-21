using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed class CreateScanCommandHandler : ICommandHandler<CreateScanCommand, ScanCreatedDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTime;
    private readonly IScanRepository _scanRepository;
    private readonly ITransportRepository _transportRepository;

    public CreateScanCommandHandler(IUnitOfWork unitOfWork, IDateTime dateTime, IScanRepository scanRepository, ITransportRepository transportRepository)
    {
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
        _transportRepository = transportRepository;
    }

    public async Task<ErrorOr<ScanCreatedDto>> Handle(CreateScanCommand request, CancellationToken cancellationToken)
    {
        var transportId = new TransportId(request.TransportId);
        var transport = await _transportRepository.GetByIdAsync(transportId, cancellationToken);
        if (transport is null)
        {
            return Failure.TransportNotExists;
        }

        var transportUnitId = new TransportUnitId(request.TransportUnitId);
        var transportUnit = transport.TransportUnits.FirstOrDefault(u => u.Id == transportUnitId);
        if (transportUnit is null)
        {
            return Failure.TransportUnitNotExists;
        }


        var scanCreateResult = Scan.Create(transportUnitId, request.DelivererId, _dateTime);
        
        if (scanCreateResult.IsError)
        {
            return scanCreateResult.Errors;
        }

        var scan = scanCreateResult.Value;
        
        //TODO: do wyciągnięcia transport na podstawie Id i weryfikacja czy jest w nim multi unit o podanym Id, jak nie jest to failure
        //szczerze to nie wiem czy dobrze. UnitDetalis w transportUnit to IUnitDetalis, więc nie mogę sprawdzić czy Amount > 0.
        if (request.Quantity is not null)
        {
            if(transportUnit.UnitDetails is not MultiUnitDetails)
            {
                return Failure.InvalidScanInput;
            }
            var result = scan.AddQuantity(request.Quantity.Value);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        if (request.LocationAccuracy is not null && request.LocationLatitude is not null &&
            request.LocationLongitude is not null)
        {
            var result = scan.AddGeolocation(request.LocationLatitude.Value, request.LocationLongitude.Value,
                                             request.LocationAccuracy.Value);
            if (result.IsError)
            {
                return result.Errors;
            }
        }

        _scanRepository.Add(scan);
        await _unitOfWork.SaveChangesAsync();
        return new ScanCreatedDto { Id = scan.Id.Value };
    }
}
