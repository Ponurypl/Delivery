namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public abstract class UnitDetails
{
    public int Id { get; set; }
    public TransportUnit TransportUnit { get; set; } = null!;
}
