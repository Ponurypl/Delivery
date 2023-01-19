namespace MultiProject.Delivery.Application.Scans.Commands.CreateScan;

public sealed record ScanGeolocation
{
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required double Accuracy { get; init; }

}