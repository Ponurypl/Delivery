using MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTransportCommand, CreateTransportRequest>();
        config.NewConfig<TransportUnitToCreate, TransportUnitRequest>();

        config.NewConfig<CreateTransportResponse, TransportCreatedDto>();
        config.NewConfig<TransportUnitResponse, TransportUnitCreatedDto>();
    }
}
