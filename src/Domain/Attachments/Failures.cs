namespace MultiProject.Delivery.Domain.Attachments;
internal class Failures
{
    public static Error InvalidAttachmentInput => Error.Validation(nameof(InvalidAttachmentInput));
}