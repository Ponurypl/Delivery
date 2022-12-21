using MultiProject.Delivery.Application.Users.Queries.GetUserLocation;

namespace MultiProject.Delivery.WebApi.v1.Users.GetUserLocation;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetUserLocationDto, GetUserLocationResponse>();
    }
}
