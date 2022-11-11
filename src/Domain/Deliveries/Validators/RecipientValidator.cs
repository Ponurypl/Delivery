using MultiProject.Delivery.Domain.Common.Extensions;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using FluentValidation;

namespace MultiProject.Delivery.Domain.Deliveries.Validators;

public sealed class RecipientValidator : AbstractValidator<Recipient>
{
    public RecipientValidator()
    {
        RuleFor(x => x.Town).NotEmpty();
        RuleFor(x => x.PostCode).NotEmpty();
        RuleFor(x => x.StreetNumber).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();

        RuleFor(x => x).Must(x => string.IsNullOrWhiteSpace(x.CompanyName) && string.IsNullOrWhiteSpace(x.Name) && string.IsNullOrWhiteSpace(x.LastName))
            .WithMessage("Atleast one recipient name must be given. Company name or personal name");

        RuleFor(x => x.LastName).NotEmpty().WhenNotEmpty(x => x.Name);
        RuleFor(x => x.Name).NotEmpty().WhenNotEmpty(x => x.LastName);
    }
}