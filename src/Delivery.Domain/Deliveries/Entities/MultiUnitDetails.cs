using Delivery.Domain.Dictionaries.Entities;

namespace Delivery.Domain.Deliveries.Entities;

public sealed class MultiUnitDetails : UnitDetails
{
    public UnitOfMeasure UnitOfMeasure { get; set; } = default!;
    public double Amount { get; set; }
}
