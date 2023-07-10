namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

public sealed record CreateAttachmentCommand : ICommand<AttachmentCreatedDto>
{
    public required Guid CreatorId { get; init; }
    public required int TransportId { get; init; }
    public int? ScanId { get; init; }
    public int? TransportUnitId { get; init; }
    public string? FileExtension { get; set; }
    public string? AdditionalInformation { get; init; }
}
