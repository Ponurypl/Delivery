using MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateAttachmentRequest, CreateAttachmentCommand>()
              .Ignore(d => d.FileExtension!);
        config.NewConfig<AttachmentCreatedDto, CreateAttachmentResponse>();
    }
}
