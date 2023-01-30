namespace MultiProject.Delivery.WebApi.v1.Scans.GetScan;

public sealed record GetScanResponse
{
    public int Id { get; init; }
    public int TransportUnitId { get; init; }
    public int Status { get; init; }
    public DateTime LastUpdateDate { get; init; }
    public Guid DelivererId { get; init; }
    public double? Quantity { get; init; }
    public ResponseGeolocationDto? Location { get; init; }
}
