using MultiProject.Delivery.Application.Common.Interfaces;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.Validators;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.Exceptions;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class Transport : IAggregateRoot
{
    private readonly List<TransportUnit> _transportUnits = new();

    public int Id { get; set; }
    public Guid DelivererId { get; set; }
    public TransportStatus Status { get; set; }
    public string Number { get; set; } = default!;
    public string? AditionalInformation { get; set; }
    public double? TotalWeight { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public Guid ManagerId { get; set; }
    public IReadOnlyList<TransportUnit> TransportUnits  => _transportUnits; 


    private Transport(Guid delivererId, string number, string? aditionalInformation, double? totalWeight, DateTime startDate,
                      Guid managerId, IDateTime dateTimeProvider)
    {
        DelivererId = delivererId;
        Status = TransportStatus.New;
        Number = number;
        AditionalInformation = aditionalInformation;
        TotalWeight = totalWeight;
        CreationDate = dateTimeProvider.Now;
        StartDate = startDate;
        ManagerId = managerId;
    }

    private void CreateTransportUnit(string? companyName, string country, string? flatNumber, string? lastName,
                                    string? name, string phoneNumber, string postCode, string? street,
                                    string streetNumber, string town, string number, string? aditionalInformation,
                                    string description, string? barcode, double? amount, UnitOfMeasure? unitOfMeasure)
    {
        Recipient recipient = Recipient.Create(companyName, country, flatNumber, lastName, name, phoneNumber,
                                         postCode, street, streetNumber, town);

        TransportUnit ntu = TransportUnit.Create(number, aditionalInformation, description, recipient,
                                                 barcode, amount, unitOfMeasure, this);
        _transportUnits.Add(ntu);
    }


    public static Result<Transport> Create(Guid delivererId, string number, string? aditionalInformation, double? totalWeight, 
        DateTime startDate, Guid managerId, IDateTime dateTimeProvider, 
        List<NewTransportUnit> transportUnitsToCreate, List<UnitOfMeasure> unitOfMeasureList)
    {
        if (unitOfMeasureList is null) throw new ArgumentNullException(nameof(unitOfMeasureList));
        if (transportUnitsToCreate is null) throw new ArgumentNullException(nameof(transportUnitsToCreate));

        TransportValidator validator = new();
        Transport newTransport = new(delivererId, number, aditionalInformation, totalWeight, startDate, managerId, dateTimeProvider);

        foreach (var unit in transportUnitsToCreate)
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
