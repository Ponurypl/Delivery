using Microsoft.EntityFrameworkCore;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Infrastructure.Persistence.Repositories;
internal sealed class UserRepository : IUserRepository
{
    private readonly DbSet<User> _users;
    public UserRepository(ApplicationDbContext context)
    {
        _users = context.Set<User>();
    }

    public void Add(User user)
    {
        _users.Add(user);
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await _users.FirstOrDefaultAsync(u => u.Id == id,cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _users.FirstOrDefaultAsync(u => u.Username == username,cancellationToken);
    }
}
