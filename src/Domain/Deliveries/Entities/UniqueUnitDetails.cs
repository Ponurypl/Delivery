using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Deliveries.Interfaces;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Entities;

public sealed class UniqueUnitDetails : Entity<UniqueUnitDetailsId>, IUnitDetails
{
    public string Barcode { get; private set; }
    public TransportUnit TransportUnit { get ; private set ; }

    private UniqueUnitDetails(UniqueUnitDetailsId id,string barcode, TransportUnit transportUnit) :base(id)
    {
        Barcode = barcode;
        TransportUnit = transportUnit;
    }

    public static ErrorOr<UniqueUnitDetails> Create(string barcode, TransportUnit transportUnit)
    {
        if (transportUnit is null) return DomainFailures.Common.MissingParentObject;
        if (string.IsNullOrWhiteSpace(barcode)) return DomainFailures.Deliveries.InvalidUniqueUnitDetails;

        return new UniqueUnitDetails(UniqueUnitDetailsId.Empty, barcode, transportUnit);
    }
}
