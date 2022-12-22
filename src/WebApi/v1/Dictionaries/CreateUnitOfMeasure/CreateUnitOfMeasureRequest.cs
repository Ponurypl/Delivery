namespace MultiProject.Delivery.WebApi.v1.Dictionaries.CreateUnitOfMeasure;

public sealed record CreateUnitOfMeasureRequest
{
    public string Name { get; init; } = default!;
    public string Symbol { get; init; } = default!;
    public string? Description { get; init; }
}
