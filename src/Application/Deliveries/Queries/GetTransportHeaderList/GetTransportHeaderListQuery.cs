namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportHeaderList;

public sealed record GetTransportHeaderListQuery : IQuery<TransportHeaderListDto>
{
    //TODO: Validator
    public required DateTime DateFrom { get; init; }
    public required DateTime DateTo { get; init; }
}
