using MultiProject.Delivery.Domain.Common.ValueTypes;

namespace MultiProject.Delivery.Application.Users.Queries.GetUserLocation;
public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AdvancedGeolocation, GetUserLocationDto>();
    }
}
