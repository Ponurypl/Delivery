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
    public UserId CreatorId { get; private set; }
    public TransportId TransportId { get; private set; }
    public ScanId? ScanId { get; private set; }
    public TransportUnitId? TransportUnitId { get; private set; }
    public AttachmentStatus Status { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public byte[]? Payload { get; private set; }
    public string? AdditionalInformation { get; private set; }

    private Attachment(AttachmentId id, UserId creatorId, TransportId transportId, AttachmentStatus status, 
                       DateTime lastUpdateDate) : base(id)
    {
        CreatorId = creatorId;
        TransportId = transportId;
        Status = status;
        LastUpdateDate = lastUpdateDate;
    }

    private static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId, IDateTime dateTimeProvider,
                                             byte[]? payload = null, string? additionalInformation = null)
    {
        if (dateTimeProvider is null) return Failures.MissingRequiredDependency;

        Attachment newAttachment = new(AttachmentId.Empty, creatorId, transportId, AttachmentStatus.Valid,
                                       dateTimeProvider.Now)
                                   {
                                       Payload = payload, AdditionalInformation = additionalInformation
                                   };

        return newAttachment;
    }

    public static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId, string additionalInformation, 
                                             IDateTime dateTimeProvider)
    {
        return Create(creatorId, transportId, dateTimeProvider, additionalInformation: additionalInformation);
    }
    public static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId, byte[] payload, IDateTime dateTimeProvider)
    {
        return Create(creatorId, transportId, dateTimeProvider, payload);
    }

    public static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId, byte[] payload, string additionalInformation, 
                                             IDateTime dateTimeProvider)
    {
        return Create(creatorId, transportId, dateTimeProvider, payload, additionalInformation);
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
