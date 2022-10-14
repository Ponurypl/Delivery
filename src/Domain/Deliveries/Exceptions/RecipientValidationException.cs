namespace MultiProject.Delivery.Domain.Deliveries.Exceptions;

public sealed class RecipientValidationException : Exception
{
    public Dictionary<string, List<string>> Errors { get; }
    
    public RecipientValidationException(Dictionary<string, List<string>> errors) 
        : base($"Validation errors encountered. Check {nameof(Errors)} property for details")
    {
        Errors = errors;
    }
}
