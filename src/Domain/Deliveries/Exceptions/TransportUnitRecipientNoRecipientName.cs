namespace MultiProject.Delivery.Domain.Deliveries.Exceptions;

public class TransportUnitRecipientNoRecipientName : Exception
{
    public TransportUnitRecipientNoRecipientName()
        : base($"Recipient company name or personal name was not specified, atleast one needs to be given.")
    {
    }
}
