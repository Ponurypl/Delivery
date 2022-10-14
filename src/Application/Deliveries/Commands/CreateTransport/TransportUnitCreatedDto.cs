namespace MultiProject.Delivery.Application.Delivieries.CreateTransport;

public sealed record TransportUnitCreatedDto
{
    public int Id { get; set; }
    public string Number { get; set; } = default!;
}