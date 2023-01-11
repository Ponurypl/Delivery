using MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateTransportRequest, CreateTransportCommand>();
        config.NewConfig<RequestTransportUnit, TransportUnitToCreate>();

        config.NewConfig<TransportCreatedDto, CreateTransportResponse>();
        config.NewConfig<TransportUnitCreatedDto, ResponseTransportUnit>();
    }
}
