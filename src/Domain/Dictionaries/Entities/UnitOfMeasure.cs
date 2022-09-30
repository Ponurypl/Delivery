namespace Delivery.Domain.Dictionaries.Entities;

public sealed class UnitOfMeasure
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string? Description { get; set; }
}
