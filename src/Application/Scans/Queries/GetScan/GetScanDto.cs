namespace MultiProject.Delivery.Application.Scans.Queries.GetScan;
public sealed record GetScanDto
{
    public int Id { get; init; }
    public int TransportUnitId { get; init; }
    public int Status { get; init; }
    public DateTime LastUpdateDate { get; init; }
    public Guid DelivererId { get; init; }
    public double? Quantity { get; init; }
    public GeolocationDto? Location { get; init; }
}
