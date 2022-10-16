using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Delivieries.CreateTransport;

public sealed class CreateTransportCommandHandler : IHandler<CreateTransportCommand, TransportCreatedDto>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IDateTime _dateTime;
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

    public CreateTransportCommandHandler(ITransportRepository transportRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, 
                                        IDateTime dateTime, IUnitOfMeasureRepository unitOfMeasureRepository)
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
        User manager = await _userRepository.GetByIdAsync(request.ManagerId);
        List<UnitOfMeasure> unitOfMeasureList = await _unitOfMeasureRepository.GetAllAsync();

        //TODO: Mapper
        List<NewTransportUnit> transportUnitsToCreate = request.TransportUnits.Select(u => new NewTransportUnit()
                                            {
                                                AditionalInformation = u.AditionalInformation,
                                                Amount = u.Amount,
                                                Barcode = u.Barcode,
                                                Description = u.Description,
                                                Number = u.Number,
                                                RecipientCompanyName = u.RecipientCompanyName,
                                                RecipientCountry = u.RecipientCountry,
                                                RecipientFlatNumber = u.RecipientFlatNumber,
                                                RecipientLastName = u.RecipientLastName,
                                                RecipientName = u.RecipientName,
                                                RecipientPhoneNumber = u.RecipientPhoneNumber,
                                                RecipientPostCode = u.RecipientPostCode,
                                                RecipientStreet = u.RecipientStreet,
                                                RecipientStreetNumber = u.RecipientStreetNumber,
                                                RecipientTown = u.RecipientTown,
                                                UnitOfMeasureId = u.UnitOfMeasureId
                                            }).ToList();


        Transport newTransport = Transport.Create(deliverer, request.Number, request.AditionalInformation, request.TotalWeight,
                                                  request.StartDate, manager, _dateTime, transportUnitsToCreate, unitOfMeasureList);

        _transportRepository.Add(newTransport);
        await _unitOfWork.SaveChangesAsync();

        return new TransportCreatedDto
        {
            Id = newTransport.Id,
            TransportUnits = newTransport.TransportUnits.Select(u => new TransportUnitCreatedDto() { Id = u.Id, Number = u.Number }).ToList()
        };
    }
}
