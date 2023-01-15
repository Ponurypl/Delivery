namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public class TransportUnitValidator : Validator<RequestTransportUnit>
{
    public TransportUnitValidator()
    {
        RuleFor(x => x)
             .Must(x => AtLeastOneTypeOfDetailsIsGiven(x) && OnlyOneTypeOfDetailsIsGiven(x))
             .WithMessage("One Type of TransportUnitDetails need to be given. MultiUnitDetail(UnitOfMeasureId + Amount) OR UniqueUnitDetail(Barcode).");

        RuleFor(x => x.Barcode).MaximumLength(20);
        RuleFor(x => x.Amount).GreaterThan(0).When(MultiUnitFieldsNotEmpty).PrecisionScale(8, 3);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Number).NotEmpty().MaximumLength(50);
        RuleFor(x => x.RecipientPhoneNumber).NotEmpty().MaximumLength(15);
        RuleFor(x => x.RecipientStreetNumber).NotEmpty().MaximumLength(5);
        RuleFor(x => x.RecipientTown).NotEmpty().MaximumLength(200);
        RuleFor(x => x.RecipientCountry).NotEmpty().MaximumLength(200);
        RuleFor(x => x.RecipientPostCode).NotEmpty().MaximumLength(200);
        RuleFor(x => x.AdditionalInformation).MaximumLength(2000);
        RuleFor(x => x.RecipientFlatNumber).MaximumLength(5);
        RuleFor(x => x.RecipientStreet).NotEmpty().MaximumLength(200);

        RuleFor(x => x.RecipientName).NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.RecipientLastName)).MaximumLength(200);
        RuleFor(x => x.RecipientLastName).NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.RecipientName)).MaximumLength(200);
        RuleFor(x => x.RecipientCompanyName).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.RecipientLastName)
                                                                        || string.IsNullOrWhiteSpace(x.RecipientName)).MaximumLength(200);
        
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
