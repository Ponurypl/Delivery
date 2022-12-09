using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class MultiUnitDetails : Entity<MultiUnitDetailsId>
{
    public UnitOfMeasureId UnitOfMeasureId { get; private set; }
    public double Amount { get; private set; }
    public TransportUnit TransportUnit { get; private set; }

#pragma warning disable CS8618, IDE0051
    private MultiUnitDetails(MultiUnitDetailsId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore

    private MultiUnitDetails(MultiUnitDetailsId id, double amount, UnitOfMeasureId unitOfMeasureId, TransportUnit transportUnit) : base(id)
    {
        Amount = amount;
        UnitOfMeasureId = unitOfMeasureId;
        TransportUnit = transportUnit;
    }

    public static ErrorOr<MultiUnitDetails> Create(double amount, UnitOfMeasureId unitOfMeasureId, TransportUnit transportUnit)
    {
        if (transportUnit is null) return DomainFailures.Common.MissingParentObject;
        if (amount <= 0) return DomainFailures.Deliveries.InvalidMultiUnitDetails;

        return new MultiUnitDetails(MultiUnitDetailsId.Empty, amount, unitOfMeasureId, transportUnit);
    }
}
