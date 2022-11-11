using FluentValidation;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Deliveries.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.Validators;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.Exceptions;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class TransportUnit : IEntity
{
    public int Id { get; set; }
    public Transport Transport { get; set; } = null!;
    public string Number { get; set; } = default!;
    public string Description { get; set; } = default!;
    public TransportUnitStatus Status { get; set; }
    public string? AditionalInformation { get; set; }
    public Recipient Recipient { get; set; } = null!;
    public UnitDetails UnitDetails { get; set; } = null!;


    private TransportUnit(string number, string? aditionalInformation, string description, Recipient recipient, 
                          Transport transport, TransportUnitStatus status)
    {
        Number = number;
        AditionalInformation = aditionalInformation;
        Description = description;
        Status = status;
        Recipient = recipient;
        Transport = transport;
    }


    public static TransportUnit Create(string number, string? aditionalInformation, string description, Recipient recipient, 
                                       string? barcode, double? amount, int? unitOfMeasureId, Transport transport)
    {
        TransportUnit newTransportUnit = new(number, aditionalInformation, description, recipient, transport, TransportUnitStatus.New);
        UnitDetails unitDetails;
        if (barcode is not null)
        {
            unitDetails = UniqueUnitDetails.Create(barcode, newTransportUnit);
        }
        else
        {
            unitDetails = MultiUnitDetails.Create(amount!.Value, unitOfMeasureId!.Value, newTransportUnit);
        }

        newTransportUnit.UnitDetails = unitDetails;

        return newTransportUnit;
    }
}
