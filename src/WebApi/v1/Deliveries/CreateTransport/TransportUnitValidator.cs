namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public class TransportUnitValidator : Validator<RequestTransportUnit>
{
    public TransportUnitValidator()
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
    private static bool AtLeastOneTypeOfDetailsIsGiven(RequestTransportUnit x)
    {
        return UniqueUnitFieldsNotEmpty(x) || MultiUnitFieldsNotEmpty(x);
    }

    private static bool OnlyOneTypeOfDetailsIsGiven(RequestTransportUnit x)
    {
        return !UniqueUnitFieldsNotEmpty(x) || !MultiUnitFieldsNotEmpty(x);
    }

    private static bool MultiUnitFieldsNotEmpty(RequestTransportUnit x)
    {
        return x.Amount.HasValue && x.UnitOfMeasureId.HasValue;
    }

    private static bool UniqueUnitFieldsNotEmpty(RequestTransportUnit x)
    {
        return !string.IsNullOrWhiteSpace(x.Barcode);
    }
}
