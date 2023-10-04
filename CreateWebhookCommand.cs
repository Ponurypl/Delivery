namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

public sealed record CreateWebhookCommand : ICommand
{
    public string url;
}
