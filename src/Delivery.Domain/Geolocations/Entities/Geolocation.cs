namespace Delivery.Domain.Geolocations.Entities;

public abstract class Geolocation
{
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public double? Accuracy { get; set; }
}
