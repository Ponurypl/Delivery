namespace MultiProject.Delivery.WebApi.v1.Scans.GetTransportUnitScans;

public sealed record GetTransportUnitScansRequest
{
    public int TransportUnitId { get; init; }
}
