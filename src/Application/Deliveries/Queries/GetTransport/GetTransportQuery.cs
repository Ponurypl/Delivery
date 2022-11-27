namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransport;

public sealed record GetTransportHeaderListQuery : IQuery<List<TransportDto>>
{
    //TODO: Validator
    public required DateTime DateFrom { get; init; }
    public required DateTime DateTo { get; init; }
}
