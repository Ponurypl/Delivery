namespace MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;
internal class CreateWebhookCommandHandler : ICommandHandler<CreateWebhookCommand>
{
    public async Task<ErrorOr<Success>> Handle(CreateWebhookCommand request, CancellationToken cancellationToken)
    {

        return Result.Success;
    }
}
