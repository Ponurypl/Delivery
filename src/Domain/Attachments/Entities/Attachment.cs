using MultiProject.Delivery.Domain.Attachments.Enums;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;
using System.Reflection.Metadata;

namespace MultiProject.Delivery.Domain.Attachments.Entities;

public sealed class Attachment : AggregateRoot<AttachmentId>
{
    public UserId CreatorId { get; set; }
    public TransportId TransportId { get; set; }
    public ScanId? ScanId { get; set; }
    public TransportUnitId? TransportUnitId { get; set; }
    public AttachmentStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public Blob? Payload { get; set; }
    public string? AdditionalInformation { get; set; }

    public Attachment(AttachmentId id) : base(id)
    {
    }
}
