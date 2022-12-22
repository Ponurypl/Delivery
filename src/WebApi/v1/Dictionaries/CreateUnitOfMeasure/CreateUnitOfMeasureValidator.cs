namespace MultiProject.Delivery.WebApi.v1.Dictionaries.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureValidator : Validator<CreateUnitOfMeasureRequest>
{
    public CreateUnitOfMeasureValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Symbol).NotEmpty();
    }
}
