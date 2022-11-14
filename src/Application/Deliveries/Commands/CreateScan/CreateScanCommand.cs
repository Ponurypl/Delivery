namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    //TODO: Dodajemy TransportId
    public int TransportUnitId { get; init; }
    public double? Quantity { get; init; }
    public Guid DelivererId { get; init; }
    public double? LocationLatitude { get; init; }
    public double? LocationLongitude { get; init; }
    public double? LocationAccuracy { get; init; }
}
