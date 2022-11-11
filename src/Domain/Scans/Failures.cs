namespace MultiProject.Delivery.Domain.Scans;

public static class Failures
{
    public static Error InvalidQuantity => Error.Validation(nameof(InvalidQuantity));
}