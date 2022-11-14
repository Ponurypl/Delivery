using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

public sealed class CreateTransportCommandHandler : ICommandHandler<CreateTransportCommand, TransportCreatedDto>
{
    private readonly ITransportRepository _transportRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTime;
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
    private readonly IUserRepository _userRepository;

    public CreateTransportCommandHandler(ITransportRepository transportRepository, IUnitOfWork unitOfWork,
                                        IDateTime dateTime, IUnitOfMeasureRepository unitOfMeasureRepository, 
                                        IUserRepository userRepository)
    {
        _transportRepository = transportRepository;
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _unitOfMeasureRepository = unitOfMeasureRepository;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<TransportCreatedDto>> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
    {
        var deliverer = await _userRepository.GetByIdAsync(new UserId(request.DelivererId));
        if (deliverer is null)
        {
            return Failure.UserNotExists;
        }

        var delivererRoleCheck = deliverer.CheckIfUserIsDeliverer();
        if(delivererRoleCheck.IsError)
        {
            return delivererRoleCheck.Errors;
        }

        var manager = await _userRepository.GetByIdAsync(new UserId(request.ManagerId));
        if (manager is null)
        {
            return Failure.UserNotExists;
        }

        var managerRoleCheck = manager.CheckIfUserIsManager();
        if (managerRoleCheck.IsError)
        { 
            return managerRoleCheck.Errors;
        }
        
        List<UnitOfMeasure> unitOfMeasureList = await _unitOfMeasureRepository.GetAllAsync();

        //TODO: Mapper - jego zostawiasz tym się zajmiemy później
        List<NewTransportUnit> transportUnitsToCreate = request.TransportUnits.Select(u => new NewTransportUnit()
                                                                   {
                                                                       AdditionalInformation = u.AdditionalInformation,
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
                                                                   })
                                                               .ToList();


        foreach (var unit in transportUnitsToCreate)
        {
            if (string.IsNullOrWhiteSpace(unit.Barcode) &&
                (unit.Amount is null or <= 0 ||
                 unit.UnitOfMeasureId is null || unitOfMeasureList.Exists(u => u.Id.Value == unit.UnitOfMeasureId)))
            {
                return Failure.InvalidTransportUnitDetails;
            }

            if (!string.IsNullOrWhiteSpace(unit.Barcode) && (unit.Amount is not null || unit.UnitOfMeasureId is not null))
            {
                return Failure.InvalidTransportUnitDetails;
            }
        }

        var newTransportResult = Transport.Create(request.DelivererId, request.Number, request.AdditionalInformation,
                                                  request.TotalWeight,
                                                  request.StartDate, request.ManagerId, _dateTime,
                                                  transportUnitsToCreate);
        if (newTransportResult.IsError)
        {
            return newTransportResult.Errors;
        }

        _transportRepository.Add(newTransportResult.Value);
        await _unitOfWork.SaveChangesAsync();

        return new TransportCreatedDto
               {
                   Id = newTransportResult.Value.Id,
                   TransportUnits = newTransportResult.Value.TransportUnits
                                                      .Select(u => new TransportUnitCreatedDto()
                                                                   {
                                                                       Id = u.Id, Number = u.Number
                                                                   })
                                                      .ToList()
               };

    }
}
