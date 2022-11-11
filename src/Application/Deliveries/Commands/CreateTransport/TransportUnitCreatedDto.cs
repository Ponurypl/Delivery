namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

public sealed record TransportUnitCreatedDto
{
    public int Id { get; init; }
    public string Number { get; init; } = default!;
}