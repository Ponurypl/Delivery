using MediatR;

namespace MultiProject.Delivery.Application.Common.Messaging;
internal interface IEvent : INotification
{
    public DateTime EventDate { get; init; }
    public string EventType { get; }
}
