using Mapster;
using MultiProject.Delivery.Application.Users.Queries.VerifyUser;

namespace MultiProject.Delivery.WebApi.v1.Users.VerifyUser;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<VerifiedUserDto, VerifyUserResponse>();
    }
}
