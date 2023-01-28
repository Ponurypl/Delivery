namespace MultiProject.Delivery.Application.Scans.Queries.GetScan;
public sealed record GeolocationDto
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
}
