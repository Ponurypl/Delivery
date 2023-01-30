namespace MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;

public sealed record GetAttachmentRequest
{
    public int AttachmentId { get; init; }
    public int TransportId { get; init; }
}
