namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed record CreateUnitOfMeasureCommand : ICommand
{    
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string? Description { get; set; }
}
