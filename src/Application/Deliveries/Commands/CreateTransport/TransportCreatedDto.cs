namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

public sealed record TransportCreatedDto
{
    public int Id { get; init; }
    public List<TransportUnitCreatedDto> TransportUnits { get; init; } = new();
}
