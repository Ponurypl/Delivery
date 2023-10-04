namespace MultiProject.Delivery.Application.Common.Persistence.Repositories;

public sealed class Webhook
{
    public Uri uri { get; private set; }
    public string name { get; private set; }
    public WebhookHandledEvent WebhookHandledEvent { get; private set; }
}

[Flags]
public enum WebhookHandledEvent
{

}