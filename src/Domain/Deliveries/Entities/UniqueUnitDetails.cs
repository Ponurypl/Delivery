namespace Delivery.Domain.Deliveries.Entities;

public sealed class UniqueUnitDetails : UnitDetails
{
    public string Barcode { get; set; } = default!;
}
