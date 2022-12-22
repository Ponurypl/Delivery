namespace MultiProject.Delivery.WebApi.v1.Dictionaries.GetUnitOfMeasure;

public sealed record GetUnitOfMeasureResponse
{
    public string Name { get; init; } = default!;
    public string Symbol { get; init; } = default!;
    public string? Description { get; init; }
}
