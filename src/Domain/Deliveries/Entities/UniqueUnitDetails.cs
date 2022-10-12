namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class UniqueUnitDetails : UnitDetails
{
    public string Barcode { get; set; } = default!;

    private UniqueUnitDetails(string barcode, TransportUnit transportUnit)
    {
        Barcode = barcode;
        TransportUnit = transportUnit;
    }

    public static UniqueUnitDetails Create(string barcode, TransportUnit transportUnit)
    {
        return new UniqueUnitDetails(barcode, transportUnit);
    }
}
