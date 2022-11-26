using MultiProject.Delivery.Domain.Attachments.Enums;

namespace MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;

public sealed record GetAttachmentQuery : IQuery<AttachmentDto>
{
    public int Id { get; init; }
}
