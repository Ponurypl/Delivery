using Delivery.Domain.Attachements.Enums;
using Delivery.Domain.Deliveries.Entities;
using Delivery.Domain.Scans.Entities;
using Delivery.Domain.Users.Entities;
using System.Reflection.Metadata;

namespace Delivery.Domain.Attachements.Entities;

public sealed class Attachement
{
    public int Id { get; set; }
    public User Creator { get; set; } = null!;
    public Transport Transport { get; set; } = null!;
    public Scan? Scan { get; set; }
    public TransportUnit? TransportUnit { get; set; }
    public AttachementStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Blob? Payload { get; set; }
    public string? AditionalInformation { get; set; }

}
