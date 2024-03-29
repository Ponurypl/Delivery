﻿using MultiProject.Delivery.Domain.Attachments.Enums;
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
    public string? FileExtension { get; private set; }
    public string? AdditionalInformation { get; private set; }

#pragma warning disable CS8618, IDE0051
    private Attachment(AttachmentId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore
    private Attachment(AttachmentId id, UserId creatorId, TransportId transportId, AttachmentStatus status, 
                       DateTime lastUpdateDate) : base(id)
    {
        CreatorId = creatorId;
        TransportId = transportId;
        Status = status;
        LastUpdateDate = lastUpdateDate;
    }

    public static ErrorOr<Attachment> Create(UserId creatorId, TransportId transportId, IDateTime dateTimeProvider,
                                             string? fileExtension = null, string? additionalInformation = null)
    {
        if (dateTimeProvider is null) return DomainFailures.Common.MissingRequiredDependency;
        if (creatorId == UserId.Empty || transportId == TransportId.Empty) return DomainFailures.Attachments.InvalidAttachment;
        if (string.IsNullOrWhiteSpace(additionalInformation) && string.IsNullOrWhiteSpace(fileExtension))
        {
            return DomainFailures.Attachments.InvalidAttachment;
        }

        Attachment newAttachment = new(AttachmentId.Empty, creatorId, transportId, AttachmentStatus.Valid,
                                       dateTimeProvider.UtcNow)
                                   {
                                       FileExtension = fileExtension, AdditionalInformation = additionalInformation
                                   };

        return newAttachment;
    }

    public ErrorOr<Updated> SetScan(TransportUnitId transportUnitId, ScanId scanId)
    {
        if (transportUnitId == Deliveries.ValueTypes.TransportUnitId.Empty || scanId == Scans.ValueTypes.ScanId.Empty)
        {
            return DomainFailures.Attachments.InvalidAttachment;
        }

        TransportUnitId = transportUnitId;
        ScanId = scanId;
        return Result.Updated;
    }

    public ErrorOr<Updated> SetTransportUnit(TransportUnitId transportUnitId)
    {
        if (transportUnitId == Deliveries.ValueTypes.TransportUnitId.Empty) return DomainFailures.Attachments.InvalidAttachment;
        TransportUnitId = transportUnitId;
        return Result.Updated;
    }

}
