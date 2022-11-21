using MultiProject.Delivery.Domain.Attachments.Enums;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Attachments.Entities;

public sealed class Attachment : AggregateRoot<AttachmentId>
{
    public UserId CreatorId { get; set; }
    public TransportId TransportId { get; set; }
    public ScanId? ScanId { get; set; }
    public TransportUnitId? TransportUnitId { get; set; }
    public AttachmentStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public byte[]? Payload { get; set; }
    public string? AdditionalInformation { get; set; }

    private Attachment(AttachmentId id, UserId creatorId, TransportId transportId, AttachmentStatus status, DateTime lastUpdateDate,
                       byte[]? payload, string? additionalInformation) : base(id)
    {
        CreatorId = creatorId;
        TransportId = transportId;
        Status = status;
        LastUpdateDate = lastUpdateDate;
        Payload = payload;
        AdditionalInformation = additionalInformation;
}

    public static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId, byte[]? payload,
                                             string? additionalInformation, IDateTime dateTimeProvider)
    {
        //TODO: Zmienić na unexpected
        if (dateTimeProvider is null) return Failures.InvalidAttachmentInput;

        Attachment newAttachment = new(AttachmentId.Empty, creatorId, transportId, AttachmentStatus.Valid,
                                       dateTimeProvider.Now, payload, additionalInformation);

        return newAttachment;
    }

    public ErrorOr<Updated> AddScanId(ScanId scanId)
    {
        ScanId = scanId;
        return Result.Updated;
    }

    public ErrorOr<Updated> AddTransportUnitId(TransportUnitId transportUnitId)
    {
        TransportUnitId = transportUnitId;
        return Result.Updated;
    }
}
