namespace MultiProject.Delivery.Domain.Dictionaries.Exceptions;

public sealed class UnitOfMeasureNotFoundException : Exception
{
    public UnitOfMeasureNotFoundException() : base("Unit of Measure was not found")
    {

    }
}
