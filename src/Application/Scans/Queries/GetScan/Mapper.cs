using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Application.Scans.Queries.GetScan;
public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Scan, GetScanDto>()
            .Map(d => d.Id, s => s.Id.Value)
            .Map(d => d.DelivererId, s => s.DelivererId.Value)
            .Map(d => d.TransportUnitId, s=> s.TransportUnitId.Value);
        config.NewConfig<Geolocation, GeolocationDto>();
    }
}
