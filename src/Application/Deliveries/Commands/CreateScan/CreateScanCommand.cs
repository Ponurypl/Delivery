namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    //TODO: Dodajemy TransportId
    public required int TransportId { get; set; }
    public required int TransportUnitId { get; init; }
    public required Guid DelivererId { get; init; }
    public double? Quantity { get; init; }
    public double? LocationLatitude { get; init; }
    public double? LocationLongitude { get; init; }
    public double? LocationAccuracy { get; init; }
}
