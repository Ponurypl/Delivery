namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;

public sealed record TransportUnitDto
{
    public int Id { get; init; }
    public string Number { get; init; } = default!;
    public int Status { get; init; }
}