namespace MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;

public sealed record GetAttachmentResponse
{
    public int Id { get; init; }
    public Guid CreatorId { get; init; }
    public int TransportId { get; init; }
    public int? ScanId { get; init; }
    public int? TransportUnitId { get; init; }
    public int Status { get; init; }
    public DateTime LastUpdateDate { get; init; }
    public string FileUrl { get; set; } 
    public string? AdditionalInformation { get; init; }
}
