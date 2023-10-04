using MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;

namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public sealed class Webhook
{
    public Uri uri { get; private set; }
    public string name { get; private set; }
    public WebhookHandledEvent WebhookHandledEvent { get; private set; }
}
