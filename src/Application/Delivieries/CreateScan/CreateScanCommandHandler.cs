using MediatR;
using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Exceptions;

namespace MultiProject.Delivery.Application.Delivieries.CreateScan;

public sealed class CreateScanCommandHandler : IHandler<CreateScanCommand, ScanCreatedDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly ITransportUnitRepository _transportUnitRepository;
    private readonly IDateTime _dateTime;
    private readonly IScanRepository _scanRepository;

    public CreateScanCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, ITransportUnitRepository transportUnitRepository, IDateTime dateTime, IScanRepository scanRepository)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _transportUnitRepository = transportUnitRepository;
        _dateTime = dateTime;
        _scanRepository = scanRepository;
    }



    public async Task<ScanCreatedDto> IRequestHandler<CreateScanCommand, ScanCreatedDto>.Handle(CreateScanCommand request, CancellationToken cancellationToken)
    {
        User deliverer = await _userRepository.GetByIdAsync(request.DelivererId);
        if (deliverer is null) throw new UserNotFoundException(nameof(request.DelivererId));

        TransportUnit transportUnit = await _transportUnitRepository.GetByIdAsync(request.TransportUnitId);
        if (transportUnit is null) throw new UserNotFoundException(nameof(request.TransportUnitId));

        Scan newScan = new()
        {
            Deliverer = deliverer,
            Geolocalization = request.Geolocalization,
            Quanitity = request.Quanitity,
            TransportUnit = transportUnit,
            LastUpdateDate = _dateTime.Now
        };

        _scanRepository.Add(newScan);
        await _unitOfWork.SaveChangesAsync();

        return new ScanCreatedDto{ Id = newScan.Id };
    }
}
