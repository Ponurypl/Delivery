namespace MultiProject.Delivery.Domain.Dictionaries;
internal class Failures
{
    public static Error InvalidUnitOfMeasureInput => Error.Validation(nameof(InvalidUnitOfMeasureInput));
}
