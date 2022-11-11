using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public interface IUserRepository
{
    void Add(User user);
    Task<User?> GetByIdAsync(Guid id);
    void Update(User user);
}
