using FluentValidation;
using MultiProject.Delivery.Application.Common.Interfaces;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.Exceptions;
using MultiProject.Delivery.Domain.Deliveries.Validators;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.Exceptions;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class Transport : IAggregateRoot
{
    private readonly List<TransportUnit> _transportUnits = new();

    public int Id { get; set; }
    public User Deliverer { get; set; } = null!;
    public TransportStatus Status { get; set; }
    public string Number { get; set; } = default!;
    public string? AditionalInformation { get; set; }
    public double? TotalWeight { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public User Manager { get; set; } = null!;
    public IReadOnlyList<TransportUnit> TransportUnits  => _transportUnits; 


    private Transport(User deliverer, string number, string? aditionalInformation, double? totalWeight, DateTime startDate,
                      User manager, IDateTime dateTimeProvider)
    {
        Deliverer = deliverer;
        Status = TransportStatus.New;
        Number = number;
        AditionalInformation = aditionalInformation;
        TotalWeight = totalWeight;
        CreationDate = dateTimeProvider.Now;
        StartDate = startDate;
        Manager = manager;
    }

    private void CreateTransportUnit(string? companyName, string country, string? flatNumber, string? lastName,
                                    string? name, string phoneNumber, string postCode, string? street,
                                    string streetNumber, string town, string number, string? aditionalInformation,
                                    string description, string? barcode, double? amount, UnitOfMeasure? unitOfMeasure)
    {
        var recipient = Recipient.Create(companyName, country, flatNumber, lastName, name, phoneNumber,
                                         postCode, street, streetNumber, town);

        TransportUnit ntu = TransportUnit.Create(number, aditionalInformation, description, recipient,
                                                 barcode, amount, unitOfMeasure, this);
        _transportUnits.Add(ntu);
    }


    public static Result<Transport> Create(User deliverer, string number, string? aditionalInformation, double? totalWeight, DateTime startDate,
        User manager, IDateTime dateTimeProvider, List<NewTransportUnit> transportUnitsToCreate, List<UnitOfMeasure> unitOfMeasureList)
    {
        TransportValidator validator = new();
       

        /*
        if (deliverer is null) return Result.Failure<Transport>(Failures.DelivererNotFound);
        if (deliverer.Role is not Users.Enums.UserRole.Deliverer) return Result.Failure<Transport>(Failures.DelivererInsufficientRole);

        if (manager is null) return Result.Failure<Transport>(Failures.ManagerNotFound);
        if (manager.Role is not Users.Enums.UserRole.Manager) return Result.Failure<Transport>(Failures.ManagerInsufficientRole);

        if (transportUnitsToCreate is null || transportUnitsToCreate.Count == 0)
        {
            //TODO: throw brak transportunitów
        }
        */


        if (unitOfMeasureList is null) throw new ArgumentNullException(nameof(unitOfMeasureList));


        Transport newTransport = new(deliverer, number, aditionalInformation, totalWeight, startDate, manager, dateTimeProvider);

        foreach (var unit in transportUnitsToCreate!)
        {
            UnitOfMeasure? unitOfMeasure = unitOfMeasureList.FirstOrDefault(u => u.Id == unit.UnitOfMeasureId);

            try
            {
                newTransport.CreateTransportUnit(unit.RecipientCompanyName, unit.RecipientCountry, unit.RecipientFlatNumber,
                                                 unit.RecipientLastName, unit.RecipientName, unit.RecipientPhoneNumber,
                                                 unit.RecipientPostCode, unit.RecipientStreet, unit.RecipientStreetNumber,
                                                 unit.RecipientTown, unit.Number, unit.AditionalInformation, unit.Description,
                                                 unit.Barcode, unit.Amount, unitOfMeasure);
            }
            catch (UnitOfMeasureNotFoundException ex)
            {
                ex.Data.Add("UnitOfMeasureId", unit.UnitOfMeasureId);
                throw;
            }            
        }


        var vResults = validator.Validate(newTransport);
        if (!vResults.IsValid)
        {
            throw new ValidationException(vResults.Errors);
        }

        return newTransport;
    }
}
