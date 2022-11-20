namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed record CreateUnitOfMeasureCommand : ICommand
{    
    public required string Name { get; init; }
    public required string Symbol { get; init; }
    public string? Description { get; init; }
}
