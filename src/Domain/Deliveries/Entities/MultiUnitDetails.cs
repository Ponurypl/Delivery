using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class MultiUnitDetails : UnitDetails
{
    public UnitOfMeasure UnitOfMeasure { get; set; } = null!;
    public double Amount { get; set; }

    private MultiUnitDetails(double amount, UnitOfMeasure unitOfMeasure, TransportUnit transportUnit)
    {
        Amount = amount;
        UnitOfMeasure = unitOfMeasure;
        TransportUnit = transportUnit;
    }

    public static MultiUnitDetails Create(double amount, UnitOfMeasure unitOfMeasure, TransportUnit transportUnit)
    {
        if (unitOfMeasure is null) throw new ArgumentNullException(nameof(unitOfMeasure));
        if (transportUnit is null) throw new ArgumentNullException(nameof(transportUnit));

        return new MultiUnitDetails(amount, unitOfMeasure, transportUnit);
    }
}
