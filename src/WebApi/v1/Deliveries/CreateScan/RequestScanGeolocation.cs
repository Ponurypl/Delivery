namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateScan;

public sealed class RequestScanGeolocation
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
}