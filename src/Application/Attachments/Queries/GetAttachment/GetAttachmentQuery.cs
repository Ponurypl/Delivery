namespace MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;

public sealed record GetAttachmentQuery : IQuery<AttachmentDto>
{
    //TODO: Albo dodać transportId albo zmienić id na guid
    public int Id { get; init; }
}
