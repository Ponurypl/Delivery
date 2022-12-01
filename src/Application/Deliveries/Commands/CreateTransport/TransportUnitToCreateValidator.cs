using FluentValidation;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;
public sealed class TransportUnitToCreateValidator : AbstractValidator<TransportUnitToCreate>
{
    public TransportUnitToCreateValidator()
    {
        RuleFor(x => x)
             .Must(x => AtLeastOneTypeOfDetailsIsGiven(x) && OnlyOneTypeOfDetailsIsGiven(x))
             .WithMessage("One Type of TransportUnitDetails need to be given. MultiUnitDetail(UnitOfMeasureId + Amount) OR UniqueUnitDetail(Barcode).");

        RuleFor(x => x.Amount).GreaterThan(0).When(MultiUnitFieldsNotEmpty);
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Number).NotEmpty();
        RuleFor(x => x.RecipientPhoneNumber).NotEmpty();
        RuleFor(x => x.RecipientStreetNumber).NotEmpty();
        RuleFor(x => x.RecipientTown).NotEmpty();
        RuleFor(x => x.RecipientCountry).NotEmpty();
        RuleFor(x => x.RecipientPostCode).NotEmpty();

        RuleFor(x => x.RecipientName).NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.RecipientLastName));
        RuleFor(x => x.RecipientLastName).NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.RecipientName));
        RuleFor(x => x.RecipientCompanyName).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.RecipientLastName)
                                                                        || string.IsNullOrWhiteSpace(x.RecipientName));
    }
    private static bool AtLeastOneTypeOfDetailsIsGiven(TransportUnitToCreate x)
    {
        return UniqueUnitFieldsNotEmpty(x) || MultiUnitFieldsNotEmpty(x);
    }

    private static bool OnlyOneTypeOfDetailsIsGiven(TransportUnitToCreate x)
    {
        return !UniqueUnitFieldsNotEmpty(x) || !MultiUnitFieldsNotEmpty(x);
    }

    private static bool MultiUnitFieldsNotEmpty(TransportUnitToCreate x)
    {
        return x.Amount is not null && x.UnitOfMeasureId is not null;
    }

    private static bool UniqueUnitFieldsNotEmpty(TransportUnitToCreate x)
    {
        return !string.IsNullOrWhiteSpace(x.Barcode);
    }
}
