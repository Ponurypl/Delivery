using Delivery.Domain.Deliveries.Entities;
using Delivery.Domain.Common.Entities;
using Delivery.Domain.Scans.Enums;
using Delivery.Domain.Users.Entities;

namespace Delivery.Domain.Scans.Entities;

public sealed class Scan
{
    public int Id { get; set; }
    public TransportUnit TransportUnit { get; set; } = null!;
    public ScanStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public double? Quanitity { get; set; }
    public User Deliverer { get; set; } = null!;
    public Geolocation? Geolocalization { get; set; }
}
