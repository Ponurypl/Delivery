namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;
public sealed record UserUpdated : IEvent
{
    public Guid Id { get; init; }
}
