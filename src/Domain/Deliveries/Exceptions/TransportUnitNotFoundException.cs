namespace MultiProject.Delivery.Domain.Deliveries.Exceptions;

public sealed class TransportUnitNotFoundException : Exception
{
    public TransportUnitNotFoundException(int id) 
        : base($"Transport unit Id {id} not found.")
    {
    }
}
