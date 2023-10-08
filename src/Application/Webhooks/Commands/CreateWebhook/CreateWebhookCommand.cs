namespace MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;

public sealed record CreateWebhookCommand : ICommand
{
    public required string Uri { get; init; }
    public EventTypeEnum EventTypeEnum { get; init; }
}