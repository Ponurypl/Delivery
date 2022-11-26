namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record ScanGelocation
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required double Accuracy { get; init; }

}