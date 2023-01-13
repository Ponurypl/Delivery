namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransports;

public sealed record GetTransportsRequest
{
    [QueryParam]
    public DateTime DateFrom { get; init; }
    [QueryParam]
    public DateTime DateTo { get; init; }
}
