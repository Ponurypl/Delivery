namespace MultiProject.Delivery.Application.Users.Services;

public interface IUserRoleService
{
    Task<bool> CheckIfUserIsDelivererAsync(Guid id);
    Task<bool> CheckIfUserIsManagerAsync(Guid id);
}