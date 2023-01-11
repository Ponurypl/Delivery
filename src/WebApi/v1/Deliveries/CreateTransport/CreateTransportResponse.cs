namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public sealed record CreateTransportResponse
{
    public int Id { get; init; }
    public List<ResponseTransportUnit> TransportUnits { get; init; } = new();
}