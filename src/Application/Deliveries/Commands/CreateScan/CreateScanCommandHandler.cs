using MediatR;
using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Exceptions;
using static MultiProject.Delivery.Application.Delivieries.CreateTransport.CreateTransportCommand;
using TransportUnit = MultiProject.Delivery.Domain.Deliveries.Entities.TransportUnit;

namespace MultiProject.Delivery.Application.Delivieries.CreateScan;

public sealed class CreateScanCommandHandler : IHandler<CreateScanCommand, ScanCreatedDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IDateTime _dateTime;
    private readonly IScanRepository _scanRepository;
    private readonly ITransportRepository _transportRepository;

    public CreateScanCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IDateTime dateTime, IScanRepository scanRepository, ITransportRepository transportRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
        _transportRepository = transportRepository;
    }



    public async Task<ScanCreatedDto> Handle(CreateScanCommand request, CancellationToken cancellationToken)
    {
        User deliverer = await _userRepository.GetByIdAsync(request.DelivererId);
        Transport transport = await _transportRepository.GetByIdAsync(request.TransportId);

        TransportUnit transportUnit = transport.TransportUnits.FirstOrDefault(u => u.Id == request.TransportId)!;
        Result<Scan> newScan = Scan.Create(transportUnit, request.Quanitity, deliverer, request.geolocation, _dateTime);

        if (newScan.IsSuccess)
        {
            _scanRepository.Add(newScan.Value);
            await _unitOfWork.SaveChangesAsync();
            return new ScanCreatedDto { Id = newScan.Value.Id };
        }
        else throw new Exception("Scan creation error" + newScan.Error);

    }
}
