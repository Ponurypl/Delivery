using MultiProject.Delivery.Domain.Common.Extensions;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Domain.Deliveries.Validators;

public class RecipientValidator : AbstractValidator<Recipient>
{
    public RecipientValidator()
    {
        RuleFor(x => x.Town).NotEmpty();
        RuleFor(x => x.PostCode).NotEmpty();
        RuleFor(x => x.StreetNumber).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();

        RuleFor(x => x).Must(x => string.IsNullOrWhiteSpace(x.CompanyName) && string.IsNullOrWhiteSpace(x.Name) && string.IsNullOrWhiteSpace(x.LastName))
            .WithMessage(""); //TODO: message

        RuleFor(x => x.LastName).NotEmpty().WhenNotEmpty(x => x.Name);
        RuleFor(x => x.Name).NotEmpty().WhenNotEmpty(x => x.LastName);
    }
}