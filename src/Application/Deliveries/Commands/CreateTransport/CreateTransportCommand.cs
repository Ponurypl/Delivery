namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

public sealed record CreateTransportCommand : ICommand<TransportCreatedDto>
{
    public Guid DelivererId { get; init; }
    public string Number { get; init; } = default!;
    public string? AdditionalInformation { get; init; }
    public double? TotalWeight { get; init; }
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
    public List<TransportUnitToCreate> TransportUnits { get; init; } = new();
}