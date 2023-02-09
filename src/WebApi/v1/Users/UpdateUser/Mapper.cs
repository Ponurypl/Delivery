using MultiProject.Delivery.Application.Users.Commands.UpdateUser;

namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUser;

public class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UpdateUserRequest, UpdateUserCommand>();
    }
}
