using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand : ICommand<UserCreatedDto>
{
    public UserRole Role { get; set; }
    public string Login { get; set; } = default!;
    public string Password { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
