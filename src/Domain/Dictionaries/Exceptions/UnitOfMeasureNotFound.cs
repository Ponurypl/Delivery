namespace MultiProject.Delivery.Domain.Dictionaries.Exceptions;

public sealed class UnitOfMeasureNotFound : Exception
{
    public UnitOfMeasureNotFound(int id) : base($"Unit of Measure for id {id} was not found")
    {

    }
}
