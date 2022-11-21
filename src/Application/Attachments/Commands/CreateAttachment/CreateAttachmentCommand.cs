namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

public sealed record CreateAttachmentCommand : ICommand<AttachmentCratedDto>
{
    public required Guid CreatorId { get; init; }
    public required int TransportId { get; init; }
    public int? ScanId { get; init; }
    public int? TransportUnitId { get; init; }
    public byte[]? Payload { get; init; }
    public string? AdditionalInformation { get; init; }
}
