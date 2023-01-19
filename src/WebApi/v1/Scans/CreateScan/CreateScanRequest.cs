namespace MultiProject.Delivery.WebApi.v1.Scans.CreateScan;

public sealed record CreateScanRequest
{
    public int TransportId { get; init; }

    public int TransportUnitId { get; init; }

    [FromClaim("UserId")]
    public Guid DelivererId { get; init; }

    public double? Quantity { get; init; }

    public RequestScanGeolocation? Location { get; init; }
}
