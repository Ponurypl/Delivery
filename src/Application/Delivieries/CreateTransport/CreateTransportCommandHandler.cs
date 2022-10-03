using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.Exceptions;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.Exceptions;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Exceptions;

namespace MultiProject.Delivery.Application.Delivieries.CreateTransport;

internal class CreateTransportCommandHandler : IHandler<CreateTransportCommand, TransportCreatedDto>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IDateTime _dateTime;
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

    public CreateTransportCommandHandler(ITransportRepository transportRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IDateTime dateTime, IUnitOfMeasureRepository unitOfMeasureRepository)
    {
        _transportRepository = transportRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _dateTime = dateTime;
        _unitOfMeasureRepository = unitOfMeasureRepository;
    }

    public async Task<TransportCreatedDto> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
    {
        User deliverer = await _userRepository.GetByIdAsync(request.DelivererId);
        if (deliverer is null) throw new UserNotFoundException(nameof(request.DelivererId));
        if (deliverer.Role is not Domain.Users.Enums.UserRole.Deliverer) throw new UserRoleException(nameof(request.DelivererId));

        User manager = await _userRepository.GetByIdAsync(request.ManagerId);
        if (manager is null) throw new UserNotFoundException(nameof(request.ManagerId));
        if (manager.Role is not Domain.Users.Enums.UserRole.Manager) throw new UserRoleException(nameof(request.ManagerId));


        Transport newTransport = new()
        {
            Deliverer = deliverer,
            Status = TransportStatus.New,
            Number = request.Number,
            AditionalInformation = request.AditionalInformation,
            TotalWeight = request.TotalWeight,
            CreationDate = _dateTime.Now,
            StartDate = request.StartDate,
            Manager = manager
        };

        _transportRepository.Add(newTransport);

        foreach (var unit in request.TransportUnits)
        {
            if (unit.Barcode is null && (unit.Amount is null || unit.UnitOfMeasureId is null))
            {
                throw new TransportUnitException(unit.Number);
            }
            if (unit.Barcode is not null && (unit.Amount is not null || unit.UnitOfMeasureId is not null))
            {
                throw new TransportUnitException(unit.Number);
            }
            if (unit.Amount is not null && unit.UnitOfMeasureId is null || unit.Amount is null && unit.UnitOfMeasureId is not null)
            {
                throw new TransportUnitException(unit.Number);
            }

            

            TransportUnit ntu = new()
            {
                Number = unit.Number,
                AditionalInformation = unit.AditionalInformation,
                Description = unit.Description,
                Status = TransportUnitStatus.New,
                Recipient = new Recipient()
                {
                    CompanyName = unit.RecipientCompanyName,
                    Country = unit.RecipientCountry,
                    FlatNumber = unit.RecipientFlatNumber,
                    LastName = unit.RecipientLastName,
                    Name = unit.RecipientName,
                    PhoneNumber = unit.RecipientPhoneNumber,
                    PostCode = unit.RecipientPostCode,
                    Street = unit.RecipientStreet,
                    StreetNumber = unit.RecipientStreetNumber,
                    Town = unit.RecipientTown
                }, 
                Transport = newTransport
            };

            // TODO: TransportUnit repository zapis

            UnitDetails unitDetails;
            if (unit.Barcode is not null)
            {
                unitDetails = new UniqueUnitDetails() { Barcode = unit.Barcode, TransportUnit = ntu };
            }
            else
            {
                UnitOfMeasure unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(unit.UnitOfMeasureId!.Value);
                if (unitOfMeasure is null) throw new UnitOfMeasureNotFound(unit.UnitOfMeasureId!.Value);
                unitDetails = new MultiUnitDetails() { Amount = unit.Amount!.Value, UnitOfMeasure = unitOfMeasure, TransportUnit = ntu };
               
            }

            // TODO: UnitDetails repository zapis
        }


        await _unitOfWork.SaveChangesAsync();

        return new TransportCreatedDto
        {
            Id = newTransport.Id,
            TransportUnits = newTransport.TransportUnits.Select(u => new TransportUnitCreatedDto() { Id = u.Id, Number = u.Number }).ToList()
        };
    }
}
