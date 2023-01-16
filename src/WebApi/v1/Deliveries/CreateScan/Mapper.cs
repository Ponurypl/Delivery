using MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateScan;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateScanRequest, CreateScanCommand>();
        config.NewConfig<RequestScanGeolocation, ScanGeolocation>();

        config.NewConfig<ScanCreatedDto, CreateScanResponse>();
    }
}
