using MultiProject.Delivery.Application.Common.Persistence.Models;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransportDbModel, TransportDto>();
    }
}
