using MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;
using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Application.Scans.Commands.CreateScan;
internal class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transport, ScanCreatedEvent>();
    }
}
