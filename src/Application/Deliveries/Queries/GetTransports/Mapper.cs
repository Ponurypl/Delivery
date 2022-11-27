using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transport, TransportDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.DelivererId, s => s.DelivererId.Value)
              .Map(d => d.Status, s => s.Status.ToString())
              .Map(d => d.ManagerId, s => s.ManagerId.Value);

    }
}
