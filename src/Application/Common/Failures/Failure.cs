using MultiProject.Delivery.Application.Users.Queries.VerifyUser;

namespace MultiProject.Delivery.Application.Common.Failures;

public static class Failure
{
    public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
    public static Error UserNotExists => Error.NotFound(nameof(UserNotExists));
    public static Error WrongUserOrPassword => Error.Validation(nameof(WrongUserOrPassword));
}