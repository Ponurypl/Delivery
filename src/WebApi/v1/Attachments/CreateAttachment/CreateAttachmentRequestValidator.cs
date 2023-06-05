namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

public sealed class CreateAttachmentRequestValidator : Validator<CreateAttachmentRequest>
{
    private readonly List<string> _allowedContentTypes =
        new()
        {
            "application/pdf",
            "image/png",
            "image/jpeg",
            "video/mp4",
            "text/plain"
        };

    public CreateAttachmentRequestValidator()
    {
        RuleFor(x => x.CreatorId).NotEmpty();
        RuleFor(x => x.TransportId).NotEmpty();
        RuleFor(x => x.AdditionalInformation).NotEmpty().When(x => x.File is null || x.File.Length == 0);
        RuleFor(x => x.File).NotEmpty().When(x => x.AdditionalInformation is null);
        RuleFor(x => x.File!.ContentType)
            .Must(x => _allowedContentTypes.Contains(x))
            .WithMessage($"Content-Type must be one of {string.Join(", ", _allowedContentTypes)}")
            .When(x => x.File is not null);
        RuleFor(x => x.TransportUnitId).NotEmpty().When(x => x.ScanId is not null);
        RuleFor(x => x.AdditionalInformation).MaximumLength(2000);
    }
}
