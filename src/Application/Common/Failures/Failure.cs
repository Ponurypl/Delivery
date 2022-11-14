namespace MultiProject.Delivery.Application.Common.Failures;

public static class Failure
{
    public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
    public static Error UserNotExists => Error.NotFound(nameof(UserNotExists));
}