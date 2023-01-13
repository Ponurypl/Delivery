namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public sealed record GetTransportDetailsRequest
{
    public int TransportId { get; init; }
}
