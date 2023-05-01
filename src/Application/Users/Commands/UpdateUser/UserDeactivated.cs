namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;

public sealed record UserDeactivated
{
    public Guid Id { get; init; }
}