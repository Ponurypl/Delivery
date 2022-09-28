using Delivery.Domain.Deliveries.Entities;
using Delivery.Domain.Geolocations.Entities;
using Delivery.Domain.Users.Entities;

namespace Delivery.Domain.Scans.Entities;

public sealed class Scan : Geolocation
{
    public int Id { get; set; }
    public TransportUnit TransportUnit { get; set; } = null!;
    public int Status { get; set; }
    public DateTime Date { get; set; }
    public double? Quanitity { get; set; }
    public User User { get; set; } = null!;


}
