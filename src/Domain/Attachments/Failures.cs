namespace MultiProject.Delivery.Domain.Attachments;
internal class Failures
{
    public static Error MissingRequiredDependency => Error.Unexpected(nameof(MissingRequiredDependency));
}