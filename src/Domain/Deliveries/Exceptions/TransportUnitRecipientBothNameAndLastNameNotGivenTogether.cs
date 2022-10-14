namespace MultiProject.Delivery.Domain.Deliveries.Exceptions;

public sealed class TransportUnitRecipientNameAndLastNameNotGivenTogether : Exception
{
    public TransportUnitRecipientNameAndLastNameNotGivenTogether(string? missingField) 
        : base($"{missingField} was not given, or is a whitespace. Both name and LastName have to be put in together")
    {
    }
}
