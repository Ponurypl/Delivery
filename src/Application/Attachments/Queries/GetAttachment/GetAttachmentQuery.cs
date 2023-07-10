namespace MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;

public sealed record GetAttachmentQuery : IQuery<AttachmentDto>
{
    public required int Id { get; init; }
    public required int TransportId { get; init; }
}
