namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public sealed class AdvancedGeolocalization : Geolocation
{
    public DateTime? GeoReadDate { get; set; }
    public double? GeoHeading { get; set; }
    public double? GeoSpeed { get; set; }
}
