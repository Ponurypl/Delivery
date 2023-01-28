namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

public sealed class CreateAttachmentRequestValidator : Validator<CreateAttachmentRequest>
{
    public CreateAttachmentRequestValidator()
    {
        RuleFor(x => x.CreatorId).NotEmpty();
        RuleFor(x => x.TransportId).NotEmpty();
        RuleFor(x => x.AdditionalInformation).NotEmpty().When(x => x.Payload is null || x.Payload.Length == 0);
        RuleFor(x => x.Payload).NotEmpty().When(x => x.AdditionalInformation is null);
        RuleFor(x => x.TransportUnitId).NotEmpty().When(x => x.ScanId is not null);
        RuleFor(x => x.AdditionalInformation).MaximumLength(2000);
    }
}
