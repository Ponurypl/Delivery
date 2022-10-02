using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    void Add(User user);
}
