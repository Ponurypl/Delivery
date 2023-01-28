namespace MultiProject.Delivery.Application.Scans.Queries.GetTransportUnitScans;
public sealed record GetTransportUnitScansQuery : IQuery<List<GetTransportUnitScansDto>>
{
    public required int Id { get; init; }
}
