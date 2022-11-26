namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportIdList;

public sealed record TransportIdListDto
{
    public List<int> Transports { get; init; }
}