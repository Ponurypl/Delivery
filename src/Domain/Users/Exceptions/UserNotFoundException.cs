namespace MultiProject.Delivery.Domain.Users.Exceptions;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(string fieldName) : base($"User not found by given ID for field {fieldName}")
    {
        
    }
}
