using MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;

namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;
public sealed record UserUpdated : IEvent
{
    public Guid Id { get; init; }
    public DateTime EventDate { get; init; }
    public string EventType { get; } = EventTypeEnum.UserUpdated.ToString();
}
