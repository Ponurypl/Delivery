namespace MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;

public sealed record GetUnitOfMeasureDto
{
    public string Name { get; init; } = default!;
    public string Symbol { get; init; } = default!;
    public string? Description { get; init; }
}