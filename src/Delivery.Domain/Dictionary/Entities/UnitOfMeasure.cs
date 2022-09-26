namespace Delivery.Domain.Dictionary.Entities;

public class UnitOfMeasure
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Unit { get; set; } = default!;
    public string? Decription { get; set; }
}
