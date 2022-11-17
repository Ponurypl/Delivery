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
    private readonly IMapper _mapper;

    public CreateTransportCommandHandler(ITransportRepository transportRepository, IUnitOfWork unitOfWork,
                                        IDateTime dateTime, IUnitOfMeasureRepository unitOfMeasureRepository, 
                                        IUserRepository userRepository, IMapper mapper)
    {
        _transportRepository = transportRepository;
        _unitOfWork = unitOfWork;
        _dateTime = dateTime;
        _unitOfMeasureRepository = unitOfMeasureRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<TransportCreatedDto>> Handle(CreateTransportCommand request, CancellationToken cancellationToken)
    {
        var delivererId = new UserId(request.DelivererId);
        var deliverer = await _userRepository.GetByIdAsync(delivererId, cancellationToken);
        if (deliverer is null)
        {
            return Failure.UserNotExists;
        }

        var delivererRoleCheck = deliverer.CheckIfUserIsDeliverer();
        if(delivererRoleCheck.IsError)
        {
            return delivererRoleCheck.Errors;
        }

        var managerId = new UserId(request.ManagerId);
        var manager = await _userRepository.GetByIdAsync(managerId, cancellationToken);
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
        List<NewTransportUnit> transportUnitsToCreate = _mapper.Map<List<NewTransportUnit>>(request.TransportUnits);

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

        var newTransportResult = Transport.Create(delivererId, request.Number, request.AdditionalInformation,
                                                  request.TotalWeight,
                                                  request.StartDate, managerId, _dateTime,
                                                  transportUnitsToCreate);
        if (newTransportResult.IsError)
        {
            return newTransportResult.Errors;
        }

        _transportRepository.Add(newTransportResult.Value);
        await _unitOfWork.SaveChangesAsync();

        return new TransportCreatedDto
               {
                   Id = newTransportResult.Value.Id.Value,
                   TransportUnits = newTransportResult.Value.TransportUnits
                                                      .Select(u => new TransportUnitCreatedDto()
                                                                   {
                                                                       Id = u.Id.Value, Number = u.Number
                                                                   })
                                                      .ToList()
               };

    }
}
