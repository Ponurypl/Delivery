namespace MultiProject.Delivery.WebApi.v1.Scans.GetScan;
public sealed record ResponseGeolocationDto
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
}
