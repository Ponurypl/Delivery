using MultiProject.Delivery.Domain.Common.Interfaces;
using MultiProject.Delivery.Domain.Deliveries.Abstractions;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class UniqueUnitDetails : UnitDetails, IEntity
{
    public string Barcode { get; private set; }

    private UniqueUnitDetails(string barcode, TransportUnit transportUnit)
    {
        Barcode = barcode;
        TransportUnit = transportUnit;
    }

    public static ErrorOr<UniqueUnitDetails> Create(string barcode, TransportUnit transportUnit)
    {
        if (string.IsNullOrWhiteSpace(barcode)) return Failures.InvalidUnitBarcode;
        if (transportUnit is null) return Failures.MissingParent;

        return new UniqueUnitDetails(barcode, transportUnit);
    }
}
