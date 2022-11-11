using MultiProject.Delivery.Domain.Common.Interfaces;
using MultiProject.Delivery.Domain.Deliveries.Abstractions;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class MultiUnitDetails : UnitDetails, IEntity
{
    public int UnitOfMeasureId { get; private set; }
    public double Amount { get; private set; }

    private MultiUnitDetails(double amount, int unitOfMeasureId, TransportUnit transportUnit)
    {
        Amount = amount;
        UnitOfMeasureId = unitOfMeasureId;
        TransportUnit = transportUnit;
    }

    public static ErrorOr<MultiUnitDetails> Create(double amount, int unitOfMeasureId, TransportUnit transportUnit)
    {
        if (transportUnit is null) return Failures.MissingParent;
        if (amount <= 0) return Failures.InvalidUnitAmount;

        return new MultiUnitDetails(amount, unitOfMeasureId, transportUnit);
    }
}
