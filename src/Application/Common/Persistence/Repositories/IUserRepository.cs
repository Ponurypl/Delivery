using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task<User?> GetByIdAsync(UserId id);
    void Update(User user);
}
