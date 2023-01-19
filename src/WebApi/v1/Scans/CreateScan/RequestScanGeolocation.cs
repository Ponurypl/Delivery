namespace MultiProject.Delivery.WebApi.v1.Scans.CreateScan;

public sealed record RequestScanGeolocation
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
}