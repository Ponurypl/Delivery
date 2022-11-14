using MultiProject.Delivery.Domain.Attachements.Enums;
using MultiProject.Delivery.Domain.Attachements.ValueTypes;
using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;
using System.Reflection.Metadata;

namespace MultiProject.Delivery.Domain.Attachements.Entities;

public sealed class Attachement : AggregateRoot<AttachementId>
{
    public UserId CreatorId { get; set; }
    public TransportId TransportId { get; set; }
    public ScanId? ScanId { get; set; }
    public TransportUnitId? TransportUnitId { get; set; }
    public AttachementStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Blob? Payload { get; set; }
    public string? AditionalInformation { get; set; }

    public Attachement(AttachementId id) : base(id)
    {
    }
}
