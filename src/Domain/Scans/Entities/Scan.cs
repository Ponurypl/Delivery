using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Scans.Enums;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Scans.Entities;

public sealed class Scan : AggregateRoot<ScanId>
{
    public TransportUnitId TransportUnitId { get; private set; }
    public ScanStatus Status { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public UserId DelivererId { get; private set; }
    public double? Quantity { get; private set; }
    public Geolocation? Location { get; private set; }

#pragma warning disable CS8618, IDE0051
    private Scan(ScanId id) : base(id)
    {
        //EF Core ctor
    }
#pragma warning restore

    private Scan(ScanId id, TransportUnitId transportUnitId, UserId delivererId, DateTime lastUpdateDate,
                 ScanStatus scanStatus)
        : base(id)
    {
        TransportUnitId = transportUnitId;
        Status = scanStatus;
        LastUpdateDate = lastUpdateDate;
        DelivererId = delivererId;
    }

    public static ErrorOr<Scan> Create(TransportUnitId transportUnitId, UserId delivererId, IDateTime dateTimeProvider)
    {
        if (transportUnitId == TransportUnitId.Empty || delivererId == UserId.Empty) return DomainFailures.Scans.InvalidScan;
        if (dateTimeProvider is null) return DomainFailures.Common.MissingRequiredDependency;

        return new Scan(ScanId.Empty, transportUnitId, delivererId, dateTimeProvider.UtcNow, ScanStatus.Valid);
    }

    public ErrorOr<Updated> AddGeolocation(double latitude, double longitude, double accuracy)
    {
        var geolocation = Geolocation.Create(latitude, longitude, accuracy);
        if (geolocation.IsError)
        {
            return geolocation.Errors;
        }

        Location = geolocation.Value;
        return Result.Updated;
    }

    public ErrorOr<Updated> AddQuantity(double quantity)
    {
        if (quantity <= 0)
        {
            return DomainFailures.Scans.InvalidScan;
        }

        Quantity = quantity;
        return Result.Updated;
    }
}