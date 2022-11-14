using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Common.Interfaces;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class Transport : IAggregateRoot
{
    private readonly List<TransportUnit> _transportUnits = new();

    public int Id { get; private set; }
    public Guid DelivererId { get; private set; }
    public TransportStatus Status { get; private set; }
    public string Number { get; private set; }
    public string? AdditionalInformation { get; private set; }
    public double? TotalWeight { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public Guid ManagerId { get; private set; }
    public IReadOnlyList<TransportUnit> TransportUnits => _transportUnits;


    private Transport(Guid delivererId, string number, string? additionalInformation, double? totalWeight, DateTime startDate,
                      Guid managerId, TransportStatus status, DateTime creationDate)
    {
        DelivererId = delivererId;
        Status = status;
        Number = number;
        AdditionalInformation = additionalInformation;
        TotalWeight = totalWeight;
        CreationDate = creationDate;
        StartDate = startDate;
        ManagerId = managerId;
    }

    public static ErrorOr<Transport> Create(Guid delivererId, string number, string? additionalInformation, double? totalWeight,
        DateTime startDate, Guid managerId, IDateTime dateTimeProvider,
        List<NewTransportUnit> transportUnitsToCreate)
    {
        if (string.IsNullOrWhiteSpace(number)) return Failures.InvalidTransportUnitInput;
        if (dateTimeProvider is null) return Failures.InvalidTransportUnitInput;
        if (transportUnitsToCreate is null || transportUnitsToCreate.Count == 0)
        {
            return Failures.InvalidTransportUnitInput;
        }
        // TODO: nie sprawdzamy czy user spełnia rolę? teoretycznie ktoś może nam tu wlepić użytkownika który nie ma prawa tu być
        
        Transport newTransport = new(delivererId, number, additionalInformation, totalWeight, startDate, managerId,
                                     TransportStatus.New, dateTimeProvider.Now);

        foreach (var unit in transportUnitsToCreate)
        {

            var tu = newTransport.CreateTransportUnit(unit.RecipientCompanyName, unit.RecipientCountry,
                                                      unit.RecipientFlatNumber,
                                                      unit.RecipientLastName, unit.RecipientName,
                                                      unit.RecipientPhoneNumber,
                                                      unit.RecipientPostCode, unit.RecipientStreet,
                                                      unit.RecipientStreetNumber,
                                                      unit.RecipientTown, unit.Number, unit.AdditionalInformation,
                                                      unit.Description,
                                                      unit.Barcode, unit.Amount, unit.UnitOfMeasureId);
            if (tu.IsError)
            {
                return tu.Errors;
            }
        }

        return newTransport;
    }

    private ErrorOr<Created> CreateTransportUnit(string? companyName, string country, string? flatNumber, string? lastName,
                                                 string? name, string phoneNumber, string postCode, string? street,
                                                 string streetNumber, string town, string number, string? additionalInformation,
                                                 string description, string? barcode, double? amount, int? unitOfMeasureId)
    {
        var recipient = Recipient.Create(companyName, country, flatNumber, lastName, name, phoneNumber,
                                         postCode, street, streetNumber, town);

        if (recipient.IsError)
        {
            return recipient.Errors;
        }

        var ntu = TransportUnit.Create(number, additionalInformation, description, recipient.Value,
                                                 barcode, amount, unitOfMeasureId, this);

        if (ntu.IsError)
        {
            return ntu.Errors;
        }

        _transportUnits.Add(ntu.Value);

        return Result.Created;
    }



}
