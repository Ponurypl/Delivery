namespace MultiProject.Delivery.Application.Scans.Commands.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    public required int TransportId { get; init; }
    public required int TransportUnitId { get; init; }
    public required Guid DelivererId { get; init; }
    public double? Quantity { get; init; }
    public ScanGeolocation? Location { get; init; }
}
