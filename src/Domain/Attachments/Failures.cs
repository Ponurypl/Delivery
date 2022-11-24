namespace MultiProject.Delivery.Domain.Attachments;
internal class Failures
{
    public static Error NoServiceProvided => Error.Unexpected(nameof(NoServiceProvided));
}