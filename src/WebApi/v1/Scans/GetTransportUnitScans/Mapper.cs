using MultiProject.Delivery.Application.Scans.Queries.GetTransportUnitScans;

namespace MultiProject.Delivery.WebApi.v1.Scans.GetTransportUnitScans;
public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetTransportUnitScansDto, GetTransportUnitScansResponse>();
        config.NewConfig<GeolocationDto, ResponseGeolocationDto>();
    }
}
