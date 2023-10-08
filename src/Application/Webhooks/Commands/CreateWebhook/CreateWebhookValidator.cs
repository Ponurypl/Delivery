using System;

namespace MultiProject.Delivery.Application.Webhooks.Commands.CreateWebhook;

public sealed class CreateWebhookValidator : AbstractValidator<CreateWebhookCommand>
{
    public CreateWebhookValidator()
    {
        RuleFor(x => x.EventTypeEnum).IsInEnum();
        RuleFor(x => x.Uri).NotEmpty()
                           .Must(CheckUri)
                           .WithMessage("Provided Uri must be a valid HTTP or HTTPS");
    }

    private static bool CheckUri(string uri)
    {
        return Uri.TryCreate(uri, UriKind.Absolute, out Uri? uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp ||
                uriResult.Scheme == Uri.UriSchemeHttps);
    }
}


