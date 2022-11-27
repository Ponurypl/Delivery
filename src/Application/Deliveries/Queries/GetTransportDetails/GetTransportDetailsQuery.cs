namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed record GetTransportDetailsQuery : IQuery<TransportDetailsDto>
{
    public required int Id { get; set; }
}