namespace MultiProject.Delivery.WebApi.v1.Dictionaries.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureValidator : Validator<CreateUnitOfMeasureRequest>
{
    public CreateUnitOfMeasureValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(50);
        RuleFor(x => x.Symbol).NotEmpty().MaximumLength(5);
        RuleFor(x => x.Description).MaximumLength(200);
    }
}
