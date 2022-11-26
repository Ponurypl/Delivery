namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportHeaderList;

public sealed record TransportHeaderListDto
{
    public List<TransportDto> Transports { get; init; }
}