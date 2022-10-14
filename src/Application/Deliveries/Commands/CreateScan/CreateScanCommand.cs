using MultiProject.Delivery.Domain.Common.ValueTypes;

namespace MultiProject.Delivery.Application.Delivieries.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    public int TransportUnitId { get; set; }
    public double? Quanitity { get; set; }
    public Guid DelivererId { get; set; }
    public Geolocation? Geolocalization { get; set; }
}
