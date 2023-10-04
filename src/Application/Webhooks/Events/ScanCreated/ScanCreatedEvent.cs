using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;

public sealed record ScanCreatedEvent : IEvent
{
    public UserId DelivererId { get; init; }
    public TransportStatus Status { get; init; }
    public string Number { get; init; }
    public string? AdditionalInformation { get; init; }
    public double? TotalWeight { get; init; }
    public DateTime CreationDate { get; init; }
    public DateTime StartDate { get; init; }
    public UserId ManagerId { get; init; }
    public int TransportUnitCount { get; init; }
    public int UncompletedTransportUnits { get; init; }
    public int CompletedTransportUnits { get; init; }
    public double? CompletionPercent { get; init; }
}

public enum TransportStatus
{
    New,
    Processing,
    Finished,
    Deleted
}