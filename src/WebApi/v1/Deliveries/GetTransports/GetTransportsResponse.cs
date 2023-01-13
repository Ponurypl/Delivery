namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransports;

public sealed record GetTransportsResponse
{
    public int Id { get; init; }
    public Guid DelivererId { get; init; }
    public string Status { get; init; } = default!;
    public string Number { get; init; } = default!;
    public DateTime CreationDate { get; init; }
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
}
