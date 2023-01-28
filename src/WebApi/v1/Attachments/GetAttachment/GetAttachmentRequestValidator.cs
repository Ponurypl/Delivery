namespace MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;

public sealed class GetAttachmentRequestValidator : Validator<GetAttachmentRequest>
{
    public GetAttachmentRequestValidator()
    {
        RuleFor(x => x.ScanId).NotEmpty();
        RuleFor(x => x.TransportId).NotEmpty();
    }
}
