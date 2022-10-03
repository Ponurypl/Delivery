namespace MultiProject.Delivery.Application.Delivieries.CreateTransport;

public sealed record TransportCreatedDto
{
    public int Id { get; set; }
    public List<TransportUnitCreatedDto> TransportUnits { get; set; } = new();
}
