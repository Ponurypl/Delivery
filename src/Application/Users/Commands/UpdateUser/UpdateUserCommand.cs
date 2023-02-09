namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;
public sealed record UpdateUserCommand : ICommand
{
    public Guid UserId { get; init; }
    public bool IsActive { get; init; }
    public UserRole Role { get; init; }
    public string PhoneNumber { get; init; } = default!;
}
