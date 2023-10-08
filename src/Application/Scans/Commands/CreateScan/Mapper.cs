using MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Scans.Commands.CreateScan;
internal class Mapper : IRegister
{

    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Transport, TransportDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.DelivererId, s => s.DelivererId.Value)
              .Map(d => d.ManagerId, s => s.ManagerId.Value)
              .Ignore(d => d.ScannedTransportUnit)
              .Ignore(d => d.CreatedScan);

        config.NewConfig<Scan, CreatedScanDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.TransportUnitId, s => s.TransportUnitId.Value)
              .Map(d => d.DelivererId, s => s.DelivererId.Value);
        config.NewConfig<TransportUnit, TransportUnitDto>()
              .Map(d => d.Id, s => s.Id.Value);
        config.NewConfig<Geolocation, GeolocationDto>();
    }
}
