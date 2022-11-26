namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportHeaderList;

public sealed record TransportDto
{
    public int Id { get; set; }
    public Guid DelivererId { get; private set; }
    public string Status { get; private set; }
    public string Number { get; private set; }
    public DateTime CreationDate { get; private set; }
    public DateTime StartDate { get; private set; }
    public Guid ManagerId { get; private set; }
}