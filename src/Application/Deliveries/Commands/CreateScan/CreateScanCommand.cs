using MultiProject.Delivery.Application.Delivieries.CreateScan;
using MultiProject.Delivery.Domain.Common.ValueTypes;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed record CreateScanCommand : ICommand<ScanCreatedDto>
{
    public int TransportUnitId { get; init; }
    public double? Quantity { get; init; }
    public Guid DelivererId { get; init; }

    //TODO: Obiekt Geolocation nie ma prawa istnieć w modelu przychodzącym z zewnątrz. Stwórz analogiczny tutaj i przepisz do prawdziwego Geolocation.
    public Geolocation? Location { get; init; }
}
