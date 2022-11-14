using MultiProject.Delivery.Domain.Scans.Enums;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Common.Interfaces;

namespace MultiProject.Delivery.Domain.Scans.Entities;

public sealed class Scan : IAggregateRoot
{
    public int Id { get; private set; }
    public int TransportUnitId { get; private set; }
    public ScanStatus Status { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public Guid DelivererId { get; private set; }
    public double? Quantity { get; private set; }
    public Geolocation? Location { get; private set; }

    private Scan(int transportUnitId, Guid delivererId, DateTime lastUpdateDate, ScanStatus scanStatus)
    {
        TransportUnitId = transportUnitId;
        Status = scanStatus;
        LastUpdateDate = lastUpdateDate;
        DelivererId = delivererId;
    }

    public static ErrorOr<Scan> Create(int transportUnitId, Guid delivererId, IDateTime dateTimeProvider)
    {
        return new Scan(transportUnitId, delivererId, dateTimeProvider.Now, ScanStatus.Valid);
    }

    public ErrorOr<Updated> AddGeolocation(double latitude, double longitude, double accuracy)
    {
        var geolocation = Geolocation.Create(latitude, latitude, accuracy);
        if (geolocation.IsError)
        {
            return geolocation.Errors;
        }

        Location = geolocation.Value;
        return Result.Updated;
    }

    public ErrorOr<Updated> AddQuantity(double quantity)
    {
        //TODO: można wskanować ilość nawet jeśli jest to  transport unit typu unique/barcode??
        if (quantity <= 0)
        {
            return Failures.InvalidQuantity;
        }

        Quantity = quantity;
        return Result.Updated;
    }
}