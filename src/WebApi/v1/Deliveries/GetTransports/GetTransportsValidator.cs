namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransports;

public sealed class GetTransportsValidator : Validator<GetTransportsRequest>
{
    public GetTransportsValidator()
    {
        RuleFor(x => x.DateFrom).NotEmpty();
        RuleFor(x => x.DateTo).NotEmpty();

        RuleFor(x => x.DateFrom).LessThanOrEqualTo(x => x.DateTo);
    }

}

