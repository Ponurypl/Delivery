using MultiProject.Delivery.Domain.Deliveries.Enums;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class TransportUnit
{
    public int Id { get; set; }
    public Transport Transport { get; set; } = null!;
    public string Description { get; set; } = default!;
    public TransportUnitStatus Status { get; set; }
    public string? AditionalInformation { get; set; }
    public Recipient Recipient { get; set; } = null!;
    public UnitDetails UnitDetails { get; set; } = null!;
}
