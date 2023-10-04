namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;

public sealed record UserDeactivated : IEvent
{
    public Guid Id { get; init; }
}