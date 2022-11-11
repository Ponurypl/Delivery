using MultiProject.Delivery.Domain.Scans.Enums;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Application.Common.Interfaces;
using MultiProject.Delivery.Domain.Scans.Validators;

namespace MultiProject.Delivery.Domain.Scans.Entities;

public sealed class Scan : IAggregateRoot
{
    public int Id { get; private set; }
    public int TransportUnitId { get; private set; }
    public ScanStatus Status { get; private set; }
    public DateTime LastUpdateDate { get; private set; }
    public double? Quanitity { get; private set; }
    public Guid DelivererId { get; private set; }
    public Geolocation? Geolocalization { get; private set; }

    private Scan(int transportUnitId, double? quanitity, Guid delivererId, Geolocation? geolocalization,
                 DateTime lastUpdateDate, ScanStatus scanStatus)
    {
        TransportUnitId = transportUnitId;
        Status = scanStatus;
        LastUpdateDate = lastUpdateDate;
        Quanitity = quanitity;
        DelivererId = delivererId;
        Geolocalization = geolocalization;
    }

    public static Result<Scan> Create(int transportUnitId, double? quanitity, Guid delivererId, 
                                      Geolocation? geolocation, IDateTime dateTimeProvider)
    {
        ScanValidator validator = new();

        Scan newScan = new(transportUnitId, quanitity, delivererId, geolocation, dateTimeProvider.Now, ScanStatus.Valid);

        var vResults = validator.Validate(newScan);
        if(!vResults.IsValid)
        {
            throw new ValidationException(vResults.Errors);
        }
        return newScan;
    }
}
