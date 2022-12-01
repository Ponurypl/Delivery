using FluentValidation;

namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;
public sealed class GetTransportQueryValidator : AbstractValidator<GetTransportsQuery>
{
    public GetTransportQueryValidator()
    {
        RuleFor(x => x.DateFrom).NotEmpty();
        RuleFor(x => x.DateTo).NotEmpty();

        RuleFor(x => x.DateFrom).LessThanOrEqualTo(x => x.DateTo);

    }
}
