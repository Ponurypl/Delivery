using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Scans.Enums;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Common.Interaces;
using MultiProject.Delivery.Application.Common.Interfaces;
using MultiProject.Delivery.Domain.Scans.Validators;

namespace MultiProject.Delivery.Domain.Scans.Entities;

public sealed class Scan : IAggregateRoot
{
    public int Id { get; set; }
    public TransportUnit TransportUnit { get; set; } = null!;
    public ScanStatus Status { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public double? Quanitity { get; set; }
    public User Deliverer { get; set; } = null!;
    public Geolocation? Geolocalization { get; set; }

    private Scan(TransportUnit transportUnit, double? quanitity, User deliverer, Geolocation? geolocalization, IDateTime dateTimeProvider)
    {
        TransportUnit = transportUnit;
        Status = ScanStatus.Valid;
        LastUpdateDate = dateTimeProvider.Now;
        Quanitity = quanitity;
        Deliverer = deliverer;
        Geolocalization = geolocalization;
    }

    public static Result<Scan> Create(TransportUnit transportUnit, double? quanitity, User deliverer, Geolocation? geolocation, IDateTime dateTimeProvider)
    {
        ScanValidator validator = new();

        Scan newScan = new(transportUnit, quanitity, deliverer, geolocation, dateTimeProvider);

        var vResults = validator.Validate(newScan);
        if(!vResults.IsValid)
        {
            throw new ValidationException(vResults.Errors);
        }
        return newScan;
    }
}
