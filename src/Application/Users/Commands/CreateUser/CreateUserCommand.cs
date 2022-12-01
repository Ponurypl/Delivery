using MultiProject.Delivery.Domain.Users.Enums;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

//TODO: Done. ALE UserRole tu mamy, a to chyba nie dobrze ? Dodać Validator
public sealed record CreateUserCommand : ICommand<UserCreatedDto>
{
    public required UserRole Role { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string PhoneNumber { get; init; }
}
