using MultiProject.Delivery.Domain.Common.Interfaces;
using MultiProject.Delivery.Domain.Deliveries.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class TransportUnit : IEntity
{
    public int Id { get; private set; }
    public Transport Transport { get; private set; }
    public string Number { get; private set; }
    public string Description { get; private set; }
    public TransportUnitStatus Status { get; private set; }
    public string? AdditionalInformation { get; private set; }
    public Recipient Recipient { get; private set; }
    public UnitDetails UnitDetails { get; private set; } = null!;


    private TransportUnit(string number, string? additionalInformation, string description, Recipient recipient, 
                          Transport transport, TransportUnitStatus status)
    {
        Number = number;
        AdditionalInformation = additionalInformation;
        Description = description;
        Status = status;
        Recipient = recipient;
        Transport = transport;
    }


    public static ErrorOr<TransportUnit> Create(string number, string? additionalInformation, string description, Recipient recipient, 
                                       string? barcode, double? amount, int? unitOfMeasureId, Transport transport)
    {
        if (string.IsNullOrWhiteSpace(number) || string.IsNullOrWhiteSpace(description) || recipient is null)
        {
            return Failures.InvalidTransportUnitInput;
        }

        if (transport is null) return Failures.MissingParent;

        if (string.IsNullOrWhiteSpace(barcode) && (amount is null or <= 0 || unitOfMeasureId is null))
        {
            return Failures.InvalidTransportUnitDetails;
        }

        if(!string.IsNullOrWhiteSpace(barcode) && (amount is not null || unitOfMeasureId is not null)) 
        {
            return Failures.InvalidTransportUnitDetails;
        }
        
        TransportUnit newTransportUnit = new(number, additionalInformation, description, recipient, transport, TransportUnitStatus.New);
        
        if (barcode is not null)
        {
            var result = UniqueUnitDetails.Create(barcode, newTransportUnit);
            if (result.IsError)
            {
                return result.Errors;
            }

            newTransportUnit.UnitDetails = result.Value;
        }
        else
        {
            var result = MultiUnitDetails.Create(amount!.Value, unitOfMeasureId!.Value, newTransportUnit);
            if (result.IsError)
            {
                return result.Errors;
            }

            newTransportUnit.UnitDetails = result.Value;
        }

        return newTransportUnit;
    }
}
