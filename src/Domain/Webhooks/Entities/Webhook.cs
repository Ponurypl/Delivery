using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Common.Extensions;
using MultiProject.Delivery.Domain.Webhooks.Enum;
using MultiProject.Delivery.Domain.Webhooks.ValueTypes;

namespace MultiProject.Delivery.Domain.Webhooks.Entities;

public sealed class Webhook : AggregateRoot<WebhookId>
{
    public Uri Uri { get; private set; }
    public WebhookHandledEvent WebhookHandledEvent { get; private set; }

#pragma warning disable CS8618, IDE0051
    public Webhook(WebhookId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore

    private Webhook(WebhookId id, Uri uri, WebhookHandledEvent handledEvent) 
        : base(id)
    {
        Uri = uri;
        WebhookHandledEvent = handledEvent;
    }

    public static ErrorOr<Webhook> Create(Uri uri, WebhookHandledEvent handledEvent)
    {
        if (uri == null) return DomainFailures.Webhooks.InvalidWebhook;
        if (!handledEvent.IsValidForEnum()) return DomainFailures.Webhooks.InvalidWebhook;

        return new Webhook(WebhookId.New(), uri, handledEvent);
    }
}