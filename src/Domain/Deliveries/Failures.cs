namespace MultiProject.Delivery.Domain.Deliveries;

internal static class Failures
{

    public static Error InvalidUnitBarcode => Error.Validation(nameof(InvalidUnitBarcode));
    public static Error MissingParent => Error.Validation(nameof(MissingParent));
    public static Error InvalidUnitAmount => Error.Validation(nameof(InvalidUnitAmount));
    public static Error InvalidTransportUnitInput => Error.Validation(nameof(InvalidTransportUnitInput));
    public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
    public static Error InvalidTransportInput => Error.Validation(nameof(InvalidTransportInput));
    public static Error InvalidRecipientInput => Error.Validation(nameof(InvalidRecipientInput));
}
