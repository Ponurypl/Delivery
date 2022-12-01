using MultiProject.Delivery.Domain.Common.Abstractions;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Domain.Dictionaries.Entities;

public sealed class UnitOfMeasure : AggregateRoot<UnitOfMeasureId>
{
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string? Description { get; set; }

    private UnitOfMeasure(UnitOfMeasureId id, string name, string symbol, string? description) 
        : base(id)
    {
        Name = name;
        Symbol = symbol;
        Description = description;
    }

    public static ErrorOr<UnitOfMeasure> Create(string name, string symbol, string? description)
    {
        if (string.IsNullOrWhiteSpace(name)) return DomainFailures.Dictionaries.InvalidUnitOfMeasure;
        if (string.IsNullOrWhiteSpace(symbol)) return DomainFailures.Dictionaries.InvalidUnitOfMeasure;

        UnitOfMeasure newUnitOfMeasure = new(UnitOfMeasureId.Empty, name, symbol, description);

        return newUnitOfMeasure;
    }
}


