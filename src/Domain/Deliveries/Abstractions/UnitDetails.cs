using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Abstractions;

public abstract class UnitDetails
{
    public int Id { get; set; }
    public TransportUnit TransportUnit { get; set; } = null!;
}
