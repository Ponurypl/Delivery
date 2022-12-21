using MultiProject.Delivery.Application.Users.Queries.GetUser;

namespace MultiProject.Delivery.WebApi.v1.Users.GetUser;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserDto, GetUserResponse>();
    }
}