namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;

public sealed record ScanCreatedEvent : IEvent
{
    public int Id { get; set; }
    public Guid DelivererId { get; init; }
    public string Status { get; init; } = default!;
    public string Number { get; init; } = default!;
    public DateTime CreationDate { get; init; }
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
    public int TransportUnitCount { get; init; }
    public int UncompletedTransportUnits { get; init; }
    public int CompletedTransportUnits { get; init; }
    public double? CompletionPercent { get; init; }
    public DateTime EventDate { get; set; }
    public TransportUnitDto ScannedTransportUnit { get; set; } = new();
    public CreatedScanDto CreatedScan { get; set; } = new();
}