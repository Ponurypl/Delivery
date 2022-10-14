namespace MultiProject.Delivery.Domain.Users.Exceptions;

public sealed class UserRoleException : Exception
{
    public UserRoleException(Guid id) : base($"Given user (Id = {id}) does not meet requiered role")
    {

    }
}
