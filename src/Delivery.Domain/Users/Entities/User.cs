using Delivery.Domain.Geolocations.Entities;

namespace Delivery.Domain.Users.Entities;

public sealed class User : Geolocation
{
    public Guid Id { get; set; }
    public int Status { get; set; }
    public DateTime? GeoReadDate { get; set; }
    public double? GeoLatitude { get; set; }
    public double? GeoLongitude { get; set; }
    public double? GeoAcuracy { get; set; }
    public double? GeoHeading { get; set; }
    public double? GeoSpeed { get; set; }
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
