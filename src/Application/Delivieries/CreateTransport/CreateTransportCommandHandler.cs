using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Deliveries.Entities;
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
    private readonly ITransportUnitRepository _transportUnitRepository;
    private readonly ITransportUnitDetailsRepository _transportUnitDetailsRepository;

    public CreateTransportCommandHandler(ITransportRepository transportRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, 
                                        IDateTime dateTime, IUnitOfMeasureRepository unitOfMeasureRepository, 
                                        ITransportUnitRepository transportUnitRepository, ITransportUnitDetailsRepository transportUnitDetailsRepository)
    {
        _transportRepository = transportRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _dateTime = dateTime;
        _unitOfMeasureRepository = unitOfMeasureRepository;
        _transportUnitRepository = transportUnitRepository;
        _transportUnitDetailsRepository = transportUnitDetailsRepository;
    }

    public async Task<TransportCreatedDto> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
    {
        User deliverer = await _userRepository.GetByIdAsync(request.DelivererId);
        User manager = await _userRepository.GetByIdAsync(request.ManagerId);

        Transport newTransport = Transport.Create(deliverer, request.Number, request.AditionalInformation, request.TotalWeight,
                                                  request.StartDate, manager, _dateTime);    
        
        _transportRepository.Add(newTransport);
        List<UnitOfMeasure> unitOfMeasureList = _unitOfMeasureRepository.GetAll

        foreach (var unit in request.TransportUnits)
        {
            UnitOfMeasure unitOfMeasure = await _unitOfMeasureRepository.GetByIdAsync(unit.UnitOfMeasureId!.Value);
            //TODO: Do optymalizacji. Wyciągnąć raz cały słownik i sprawdzać w pamięci

            var recipient = Recipient.Create(unit.RecipientCompanyName, unit.RecipientCountry, unit.RecipientFlatNumber,
                                             unit.RecipientLastName, unit.RecipientName, unit.RecipientPhoneNumber,
                                             unit.RecipientPostCode, unit.RecipientStreet, unit.RecipientStreetNumber, 
                                             unit.RecipientTown);

            TransportUnit ntu = TransportUnit.Create(unit.Number, unit.AditionalInformation, unit.Description, recipient,
                                                     unit.Barcode, unit.Amount, unitOfMeasure);
            //TODO: relacja transport i transportUnit
            // TODO: do przeróbki repozytoria
            _transportUnitRepository.Add(ntu);            
            _transportUnitDetailsRepository.Add(ntu.UnitDetails);
        }

        await _unitOfWork.SaveChangesAsync();

        return new TransportCreatedDto
        {
            Id = newTransport.Id,
            TransportUnits = newTransport.TransportUnits.Select(u => new TransportUnitCreatedDto() { Id = u.Id, Number = u.Number }).ToList()
        };
    }
}
