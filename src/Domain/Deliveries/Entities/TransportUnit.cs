using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class TransportUnit : Entity<TransportUnitId>
{
    public Transport Transport { get; private set; }
    public string Number { get; private set; }
    public string Description { get; private set; }
    public TransportUnitStatus Status { get; private set; }
    public string? AdditionalInformation { get; private set; }
    public Recipient Recipient { get; private set; }
    public UniqueUnitDetails? UniqueUnitDetails { get; private set; }
    public MultiUnitDetails? MultiUnitDetails { get; private set; }

#pragma warning disable CS8618, IDE0051
    private TransportUnit(TransportUnitId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore

    private TransportUnit(TransportUnitId id, string number, string? additionalInformation, string description, Recipient recipient, 
                          Transport transport, TransportUnitStatus status) : base(id)
    {
        Number = number;
        AdditionalInformation = additionalInformation;
        Description = description;
        Status = status;
        Recipient = recipient;
        Transport = transport;
    }


    public static ErrorOr<TransportUnit> Create(string number, string? additionalInformation, string description, Recipient recipient, 
                                       string? barcode, double? amount, UnitOfMeasureId? unitOfMeasureId, Transport transport)
    {
        if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(description) || recipient is null)
        {
            return DomainFailures.Deliveries.InvalidTransportUnit;
        }

        if (transport is null) return DomainFailures.Common.MissingParentObject;

        if (string.IsNullOrWhiteSpace(barcode) && (amount is null or <= 0 || unitOfMeasureId is null))
        {
            return DomainFailures.Deliveries.InvalidTransportUnitDetails;
        }

        if(!string.IsNullOrWhiteSpace(barcode) && (amount is not null || unitOfMeasureId is not null)) 
        {
            return DomainFailures.Deliveries.InvalidTransportUnitDetails;
        }
        
        TransportUnit newTransportUnit = new(TransportUnitId.Empty, number, additionalInformation, description, recipient, transport, TransportUnitStatus.New);
        
        if (barcode is not null)
        {
            var result = UniqueUnitDetails.Create(barcode, newTransportUnit);
            if (result.IsError)
            {
                return result.Errors;
            }

            newTransportUnit.UniqueUnitDetails = result.Value;
        }
        else
        {
            var result = MultiUnitDetails.Create(amount!.Value, unitOfMeasureId!.Value, newTransportUnit);
            if (result.IsError)
            {
                return result.Errors;
            }

            newTransportUnit.MultiUnitDetails = result.Value;
        }

        return newTransportUnit;
    }
}
