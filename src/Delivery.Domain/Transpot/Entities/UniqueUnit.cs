namespace Delivery.Domain.Transpot.Entities;

public class UniqueUnit
{
    public int Id { get; set; }
    public int TransportUnitId { get; set; }
    public string Barcode { get; set; } = default!;
}
