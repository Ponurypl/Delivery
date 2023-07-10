namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

public sealed class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
{
    public CreateAttachmentCommandValidator()
    {
        RuleFor(x => x.CreatorId).NotEmpty();
        RuleFor(x => x.TransportId).NotEmpty();
        RuleFor(x => x.AdditionalInformation).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.FileExtension));
        RuleFor(x => x.FileExtension).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.AdditionalInformation));
        RuleFor(x => x.TransportUnitId).NotEmpty().When(x => x.ScanId is not null);
        RuleFor(x => x.AdditionalInformation).MaximumLength(2000);
    }
}