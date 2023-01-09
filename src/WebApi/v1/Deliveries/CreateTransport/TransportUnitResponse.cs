namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public sealed record TransportUnitResponse
{
    public int Id { get; init; }
    public string Number { get; init; } = default!;
}
