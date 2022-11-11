using MultiProject.Delivery.Application.Common.Persistence.Repositories;

namespace MultiProject.Delivery.Application.Users.Services;

internal class UserRoleService : IUserRoleService
{

    private readonly IUserRepository _userRepository;

    public UserRoleService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> CheckIfUserIsDelivererAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user?.Role is Domain.Users.Enums.UserRole.Deliverer;
    }


    public async Task<bool> CheckIfUserIsManagerAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user?.Role is Domain.Users.Enums.UserRole.Manager;
    }
}
