namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportIdList;
public sealed record GetTransportIdListQuery : IQuery<TransportIdListDto>
{
    //TODO: Validator
    public required DateTime DateFrom { get; init; }
    public required DateTime DateTo { get; init; }
}
