namespace MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;

[Flags]
public enum EventTypeEnum
{
    ScanCreated,
    UserUpdated,
    UserDeactivated
}