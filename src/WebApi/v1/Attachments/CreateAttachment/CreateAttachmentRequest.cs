namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

/// <summary>
/// Atleast one Payload or AdditionalInformation needs to be provided
/// If ScanId is given, then TranportUnit is mandatory
/// </summary>
public sealed record CreateAttachmentRequest
{
    public Guid CreatorId { get; init; }
    public int TransportId { get; init; }
    public int? ScanId { get; init; }
    public int? TransportUnitId { get; init; }
    public byte[]? Payload { get; init; }
    public string? AdditionalInformation { get; init; }
}
