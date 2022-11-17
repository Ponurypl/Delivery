using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed record CreateUserCommand : ICommand<UserCreatedDto>
{
    public UserRole Role { get; init; }
    public string Username { get; init; } = default!;
    public string Password { get; init; } = default!;
    public string PhoneNumber { get; init; } = default!;
}
