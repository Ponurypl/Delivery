using MultiProject.Delivery.Domain.Attachments.Entities;

namespace MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Attachment, AttachmentDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.CreatorId, s => s.CreatorId.Value)
              .Map(d => d.TransportId, s => s.TransportId.Value)
              //TODO: chuj nie wiem jak wyciągnąc to, mapster zawsze krzyczy że się nie da.
              .Map(d => d.TransportUnitId, s => s.TransportUnitId.Value.Value, s => s.TransportUnitId.HasValue)
              .Map(d => d.ScanId, s => s.ScanId.Value.Value, s => s.ScanId.HasValue)
              .Map(d => d.Status, s => (int)s.Status);
    }
}
