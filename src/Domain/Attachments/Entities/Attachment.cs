using MultiProject.Delivery.Domain.Attachments.Enums;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
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

    private Attachment(AttachmentId id, UserId creatorId, TransportId transportId, ScanId? scanId,
                       TransportUnitId? transportUnitId, AttachmentStatus status, DateTime lastUpdateDate,
                       Blob? payload, string? additionalInformation) : base(id)
    {
        CreatorId = creatorId;
        TransportId = transportId;
        ScanId = scanId;
        TransportUnitId = transportUnitId;
        Status = status;
        LastUpdateDate = lastUpdateDate;
        Payload = payload;
        AdditionalInformation = additionalInformation;
}
    public static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId,
                                             ScanId? scanId, TransportUnitId? transportUnitId,
                                             Blob? payload, string? additionalInformation, IDateTime dateTimeProvider, CancellationToken cancellationToken = default)
    {
        //TODO: jest sens zrzucać tu cancellation token? za dużo się tu nie dzieje
        if (dateTimeProvider is null) return Failures.InvalidAttachmentInput;

        Attachment newAttachment = new(AttachmentId.Empty, creatorId, transportId, scanId, transportUnitId, AttachmentStatus.Valid, dateTimeProvider.Now, payload, additionalInformation);

        return newAttachment;
    }
}
