using MediatR;

namespace MultiProject.Delivery.Application.Common.Messaging;
internal interface IEventHandler<in TEvent> : INotificationHandler<TEvent>
    where TEvent : IEvent
{
}
