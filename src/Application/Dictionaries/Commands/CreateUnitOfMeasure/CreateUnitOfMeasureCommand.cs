namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed record CreateUnitOfMeasureCommand : ICommand
{    
    public string Name { get; init; } = default!;
    public string Symbol { get; init; } = default!;
    public string? Description { get; init; }
}
