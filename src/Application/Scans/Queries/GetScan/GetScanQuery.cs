namespace MultiProject.Delivery.Application.Scans.Queries.GetScan;
public sealed record GetScanQuery : IQuery<GetScanDto>
{
    public required int Id { get; init; }
}
