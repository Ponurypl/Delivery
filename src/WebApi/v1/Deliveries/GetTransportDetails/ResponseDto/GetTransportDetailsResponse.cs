namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public sealed record GetTransportDetailsResponse
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
    public List<ResponseTransportUnitDto> TransportUnits { get; init; } = new();
    public List<int> Attachements { get; init; } = new();
}
