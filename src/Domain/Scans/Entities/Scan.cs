using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Common.Entities;
using MultiProject.Delivery.Domain.Scans.Enums;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Domain.Scans.Entities;

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
