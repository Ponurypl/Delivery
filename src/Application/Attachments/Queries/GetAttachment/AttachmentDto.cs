﻿namespace MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;

public sealed record AttachmentDto
{
    public int Id { get; init; }
    public Guid CreatorId { get; init; }
    public int TransportId { get; init; }
    public int? ScanId { get; init; }
    public int? TransportUnitId { get; init; }
    public int Status { get; init; }
    public DateTime LastUpdateDate { get; init; }
    public string? AdditionalInformation { get; init; }
    public string? FileExtension { get; set; }
}
