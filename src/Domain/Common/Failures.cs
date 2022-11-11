namespace MultiProject.Delivery.Domain.Common;

public static class Failures
{
    public static Error InvalidGeolocation => Error.Validation(nameof(InvalidGeolocation));
    public static Error InvalidAdvancedGeolocation => Error.Validation(nameof(InvalidAdvancedGeolocation));
}