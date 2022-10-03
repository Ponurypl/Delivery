namespace MultiProject.Delivery.Domain.Users.Exceptions;

public sealed class UserRoleException : Exception
{
    public UserRoleException(string fieldName) : base($"Given user in {fieldName} does not meet requiered role")
    {

    }
}
