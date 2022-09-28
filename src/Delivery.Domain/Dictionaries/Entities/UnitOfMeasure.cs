namespace Delivery.Domain.Dictionaries.Entities;

public sealed class UnitOfMeasure
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public string? Decription { get; set; }
}
