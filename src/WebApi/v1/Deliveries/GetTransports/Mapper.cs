using MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransports;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetTransportsRequest, GetTransportsQuery>();
        config.NewConfig<TransportDto, GetTransportsResponse>();
    }
}
