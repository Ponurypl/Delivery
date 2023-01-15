namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

public sealed class GetTransportsQueryValidator : AbstractValidator<GetTransportsQuery>
{
    public GetTransportsQueryValidator()
    {
        RuleFor(x => x.DateFrom).NotEmpty();
        RuleFor(x => x.DateTo).NotEmpty();

        RuleFor(x => x.DateFrom).LessThanOrEqualTo(x => x.DateTo);

    }
}
