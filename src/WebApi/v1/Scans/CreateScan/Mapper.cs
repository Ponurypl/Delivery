using MultiProject.Delivery.Application.Scans.Commands.CreateScan;

namespace MultiProject.Delivery.WebApi.v1.Scans.CreateScan;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateScanRequest, CreateScanCommand>();
        config.NewConfig<RequestScanGeolocation, ScanGeolocation>();

        config.NewConfig<ScanCreatedDto, CreateScanResponse>();
    }
}
