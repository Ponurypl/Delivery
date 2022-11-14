namespace MultiProject.Delivery.Application.Deliveries;
internal class Failures
{
    public static Error UserDoesNotMeetRequieredRole => Error.Validation(nameof(UserDoesNotMeetRequieredRole));
    public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
}
