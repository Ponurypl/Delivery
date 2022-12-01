namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

public sealed record GetTransportsQuery : IQuery<List<TransportDto>>
{
    public required DateTime DateFrom { get; init; }
    public required DateTime DateTo { get; init; }
}
