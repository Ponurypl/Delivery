namespace MultiProject.Delivery.Application.Users;

public static class Failures
{
    public static Error UserNotExists => Error.NotFound(nameof(UserNotExists));
}