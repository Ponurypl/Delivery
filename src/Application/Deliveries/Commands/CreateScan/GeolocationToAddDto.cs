namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record GeolocationToAddDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Accuracy { get; set; }
}
