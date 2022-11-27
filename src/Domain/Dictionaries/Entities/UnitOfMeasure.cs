using MultiProject.Delivery.Domain.Common.Interfaces;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Domain.Dictionaries.Entities;

public sealed class UnitOfMeasure : IAggregateRoot
{
    public UnitOfMeasureId Id { get; set; }
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
        if (string.IsNullOrWhiteSpace(name)) return DomainFailures.Dictionaries.InvalidUnitOfMeasure;
        if (string.IsNullOrWhiteSpace(symbol)) return DomainFailures.Dictionaries.InvalidUnitOfMeasure;

        UnitOfMeasure newUnitOfMeasure = new(name, symbol, description);

        return newUnitOfMeasure;
    }
}


