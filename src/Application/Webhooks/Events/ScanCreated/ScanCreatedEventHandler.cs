namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;
internal class ScanCreatedEventHandler : IEventHandler<ScanCreatedEvent>
{
    public Task Handle(ScanCreatedEvent notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
