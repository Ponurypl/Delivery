using MultiProject.Delivery.Domain.Attachements.Enums;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Users.Entities;
using System.Reflection.Metadata;

namespace MultiProject.Delivery.Domain.Attachements.Entities;

public sealed class Attachement : IAggregateRoot
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
