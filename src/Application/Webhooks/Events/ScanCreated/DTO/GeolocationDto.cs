namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;

public sealed record GeolocationDto
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
}