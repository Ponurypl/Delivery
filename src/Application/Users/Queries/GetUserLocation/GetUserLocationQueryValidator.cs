namespace MultiProject.Delivery.Application.Users.Queries.GetUserLocation;

public sealed class GetUserLocationQueryValidator : AbstractValidator<GetUserLocationQuery>
{
    public GetUserLocationQueryValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
