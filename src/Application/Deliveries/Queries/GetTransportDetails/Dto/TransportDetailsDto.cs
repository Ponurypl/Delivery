namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed record TransportDetailsDto
{
    public int Id { get; init; }
    public Guid DelivererId { get; init; }
    public string Status { get; init; } = default!;
    public string Number { get; init; } = default!;
    public string? AdditionalInformation { get; init; }
    public double? TotalWeight { get; init; }
    public DateTime CreationDate { get; init; }
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
    public List<TransportUnitDto> TransportUnits { get; init; } = new();
    public List<int> Attachements { get; init; } = new();
}