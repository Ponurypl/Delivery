namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;

public sealed record CreatedScanDto
{
    public int Id { get; init; }
    public int TransportUnitId { get; init; }
    public int Status { get; init; }
    public DateTime LastUpdateDate { get; init; }
    public Guid DelivererId { get; init; }
    public double? Quantity { get; init; }
    public GeolocationDto? Location { get; init; }
}