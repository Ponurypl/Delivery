using MultiProject.Delivery.Application.Scans.Queries.GetScan;

namespace MultiProject.Delivery.WebApi.v1.Scans.GetScan;
public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetScanDto, GetScanResponse>();
        config.NewConfig<GeolocationDto, ResponseGeolocationDto>();
    }
}
