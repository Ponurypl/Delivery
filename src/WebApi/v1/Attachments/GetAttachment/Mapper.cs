using MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;

namespace MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AttachmentDto, GetAttachmentResponse>().Ignore(x => x.FileUrl);
    }
}
