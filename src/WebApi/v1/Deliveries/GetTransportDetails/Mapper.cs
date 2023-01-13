using MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<TransportUnitDto, ResponseTransportUnitDto>();
        config.NewConfig<RecipientDto, ResponseRecipientDto>();
        config.NewConfig<TransportDetailsDto, GetTransportDetailsResponse>();

    }
}
