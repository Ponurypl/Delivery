using MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;

namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;

public sealed record ScanCreatedEvent : IEvent
{
    public DateTime EventDate { get; init; }
    public string EventType { get; } = EventTypeEnum.ScanCreated.ToString();
    public TransportDto ScannedTransport { get; set; } = new();
}