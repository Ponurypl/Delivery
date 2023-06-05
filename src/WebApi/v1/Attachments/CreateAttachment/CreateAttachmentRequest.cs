namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

/// <summary>
/// At least one of information should be specified (File or AdditionalInformation)
/// If ScanId is given, then TransportUnit is mandatory
/// </summary>
public sealed record CreateAttachmentRequest
{
    public Guid CreatorId { get; init; }
    public int TransportId { get; init; }
    public int? ScanId { get; init; }
    public int? TransportUnitId { get; init; }
    public IFormFile? File { get; init; }
    public string? AdditionalInformation { get; init; }
}
