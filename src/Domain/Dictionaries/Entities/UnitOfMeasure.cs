using MultiProject.Delivery.Domain.Common.Interfaces;

namespace MultiProject.Delivery.Domain.Dictionaries.Entities;

public sealed class UnitOfMeasure : IAggregateRoot
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string? Description { get; set; }

    private UnitOfMeasure(string name, string symbol, string? description)
    {
        Name = name;
        Symbol = symbol;
        Description = description;
    }

    public static ErrorOr<UnitOfMeasure> Create(string name, string symbol, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) return Failures.InvalidUnitOfMeasureInput;
        if (string.IsNullOrWhiteSpace(symbol)) return Failures.InvalidUnitOfMeasureInput;

        UnitOfMeasure newUnitOfMeasure = new(name, symbol, description);

        return newUnitOfMeasure;
    }
}


