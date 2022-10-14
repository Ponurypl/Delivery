using MultiProject.Delivery.Domain.Common.Interaces;

namespace MultiProject.Delivery.Domain.Dictionaries.Entities;

public sealed class UnitOfMeasure : IAggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string? Description { get; set; }
}
