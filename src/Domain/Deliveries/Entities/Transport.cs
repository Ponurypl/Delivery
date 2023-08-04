using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class Transport : AggregateRoot<TransportId>
{
    private readonly List<TransportUnit> _transportUnits = new();

    public UserId DelivererId { get; private set; }
    public TransportStatus Status { get; private set; }
    public string Number { get; private set; }
    public string? AdditionalInformation { get; private set; }
    public double? TotalWeight { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public UserId ManagerId { get; private set; }
    public IReadOnlyList<TransportUnit> TransportUnits => _transportUnits;

#pragma warning disable CS8618, IDE0051
    private Transport(TransportId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore

    private Transport(TransportId id, UserId delivererId, string number, string? additionalInformation, double? totalWeight, DateTime startDate,
                      UserId managerId, TransportStatus status, DateTime creationDate) : base(id)
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

    public static ErrorOr<Transport> Create(UserId delivererId, string number, string? additionalInformation,
                                            double? totalWeight, DateTime startDate, UserId managerId, 
                                            IDateTime dateTimeProvider, List<NewTransportUnit> transportUnitsToCreate)
    {
        if (dateTimeProvider is null) return DomainFailures.Common.MissingRequiredDependency;

        DateTime creationDate = dateTimeProvider.UtcNow;

        if (string.IsNullOrWhiteSpace(number)
            || delivererId == UserId.Empty
            || managerId == UserId.Empty
            || startDate == default
            || creationDate == default
            || startDate < creationDate
            || transportUnitsToCreate is null
            || transportUnitsToCreate.Count == 0)
        {
            return DomainFailures.Deliveries.InvalidTransport;
        }

        List<NewTransportUnit> uniqueTypeTransportUnit = transportUnitsToCreate.Where(u => u.Barcode is not null).ToList();
        if (transportUnitsToCreate.Select(u => u.Number).Distinct().Count() != transportUnitsToCreate.Count
            || uniqueTypeTransportUnit.Select(u => u.Barcode).Distinct().Count() != uniqueTypeTransportUnit.Count)
        {
            return DomainFailures.Deliveries.InvalidTransportUnit;
        }


        Transport newTransport = new(TransportId.Empty, delivererId, number, additionalInformation,
                                     totalWeight, startDate, managerId,
                                     TransportStatus.New, creationDate);

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
                                                      unit.Barcode, unit.Amount,
                                                      unit.UnitOfMeasureId is null
                                                          ? null
                                                          : new UnitOfMeasureId(unit.UnitOfMeasureId.Value));
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
                                                 string description, string? barcode, double? amount, UnitOfMeasureId? unitOfMeasureId)
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

    public ErrorOr<Success> CheckIfScannable()
    {
        if (Status is TransportStatus.Deleted or TransportStatus.Finished)
        {
            return DomainFailures.Deliveries.TransportStatusError;
        }
        return Result.Success;
    }

}
