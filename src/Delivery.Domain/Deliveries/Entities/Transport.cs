using Delivery.Domain.Users.Entities;

namespace Delivery.Domain.Deliveries.Entities;

public sealed class Transport
{
    public int Id { get; set; }
    public User DelivererId { get; set; } = null!;
    public int Status { get; set; }
    public string Number { get; set; } = default!;
    public string? AditionalInformation { get; set; }
    public double? TotalWeight { get; set; }
    public DateTime CreationDate { get; set; }
    public DateTime StartDate { get; set; }
    public User ManagerId { get; set; } = null!;
    public List<TransportUnit> TransportUnits { get; set; } = new();
}
