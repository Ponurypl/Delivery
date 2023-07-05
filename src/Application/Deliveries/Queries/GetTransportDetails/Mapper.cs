using MultiProject.Delivery.Application.Common.Persistence.Models;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RecipientDbModel, RecipientDto>();
        config.NewConfig<TransportUnitDbModel, TransportUnitDto>()
              .Ignore(d => d.Attachments)
              .Ignore(d => d.Scans);

        config.NewConfig<TransportDbModel, TransportDetailsDto>()
              .Ignore(d => d.Attachments)
              .Ignore(d => d.TransportUnits);
    }
}