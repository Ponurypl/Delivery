using FluentValidation;

namespace MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

public sealed class CreateAttachmentCommandValidator : AbstractValidator<CreateAttachmentCommand>
{
    public CreateAttachmentCommandValidator()
    {
        RuleFor(x => x.CreatorId).NotEmpty();
        RuleFor(x => x.AdditionalInformation).NotEmpty().When(x => x.Payload is null || x.Payload.Length == 0);
        RuleFor(x=> x.Payload).NotEmpty().When(x => x.AdditionalInformation is null);
    }
}