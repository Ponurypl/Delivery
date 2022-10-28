using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Application.Users.Services;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Application.Delivieries.CreateTransport;

public sealed class CreateTransportCommandHandler : IHandler<CreateTransportCommand, TransportCreatedDto>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRoleService _userRoleService;
    private readonly IDateTime _dateTime;
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;

    public CreateTransportCommandHandler(ITransportRepository transportRepository, IUnitOfWork unitOfWork,
                                        IDateTime dateTime, IUnitOfMeasureRepository unitOfMeasureRepository, 
                                        IUserRoleService userRoleService)
    {
        _transportRepository = transportRepository;
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _unitOfMeasureRepository = unitOfMeasureRepository;
        _userRoleService = userRoleService;
    }

    public async Task<TransportCreatedDto> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
    {
        if (await _userRoleService.CheckIfUserIsDelivererAsync(request.DelivererId) == false)
        {
            //TODO: Error o nie spełnionej roli
        }
        if (await _userRoleService.CheckIfUserIsManagerAsync(request.ManagerId) == false)
        {
            //TODO: Error o nie spełnionej roli
        }

        //TODO: ???
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


        Result<Transport> newTransportResult = Transport.Create(request.DelivererId, request.Number, request.AditionalInformation, request.TotalWeight,
                                                  request.StartDate, request.ManagerId, _dateTime, transportUnitsToCreate, unitOfMeasureList);
        if (newTransportResult.IsSuccess)
        {
            _transportRepository.Add(newTransportResult.Value);
            await _unitOfWork.SaveChangesAsync();

            return new TransportCreatedDto
            {
                Id = newTransportResult.Value.Id,
                TransportUnits = newTransportResult.Value.TransportUnits.Select(u => new TransportUnitCreatedDto() { Id = u.Id, Number = u.Number }).ToList()
            };
        }
        else throw new Exception("Transport creation error" + newTransportResult.Error);
    }
}
