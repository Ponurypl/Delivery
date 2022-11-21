using System.Reflection.Metadata;

namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;
public sealed record CreateAttachmentCommand : ICommand<AttachmentCratedDto>
{
    public Guid CreatorId { get; set; }
    public int TransportId { get; set; }
    public int? ScanId { get; set; }
    public int? TransportUnitId { get; set; }
    public Blob? Payload { get; set; }
    public string? AdditionalInformation { get; set; }
}
