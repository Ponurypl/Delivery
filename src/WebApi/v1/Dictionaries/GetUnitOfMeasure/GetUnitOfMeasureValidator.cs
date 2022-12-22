namespace MultiProject.Delivery.WebApi.v1.Dictionaries.GetUnitOfMeasure;

public class GetUnitOfMeasureValidator : Validator<GetUnitOfMeasureRequest>
{
    public GetUnitOfMeasureValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}
