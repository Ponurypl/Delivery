using Mapster;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Users.Queries.GetUser;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>()
              .Map(d => d.Id, s => s.Id.Value)
              .Map(d => d.Role, s => (int) s.Role);
    }
}