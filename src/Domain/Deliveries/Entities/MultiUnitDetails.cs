using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class MultiUnitDetails : UnitDetails
{
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
    public double Amount { get; set; }
}
