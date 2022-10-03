namespace MultiProject.Delivery.Domain.Deliveries.Exceptions;

public sealed class TransportUnitException : Exception
{
    public TransportUnitException(string number) 
        : base($"Transport Unit {number} not specified properly. You should specify Barcode or UnitOfMeasure together with Amount.")
    {

    }
}
