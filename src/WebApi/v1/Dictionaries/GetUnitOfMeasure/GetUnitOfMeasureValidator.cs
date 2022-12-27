namespace MultiProject.Delivery.WebApi.v1.Dictionaries.GetUnitOfMeasure;

public sealed class GetUnitOfMeasureValidator : Validator<GetUnitOfMeasureRequest>
{
    public GetUnitOfMeasureValidator()
    {
        RuleFor(x => x.UnitOfMeasureId).NotEmpty();
    }
}
