namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    public required int TransportId { get; init; }
    public required int TransportUnitId { get; init; }
    public required Guid DelivererId { get; init; }
    public double? Quantity { get; init; }
    public double? LocationLatitude { get; init; }
    public double? LocationLongitude { get; init; }
    public double? LocationAccuracy { get; init; }
}
