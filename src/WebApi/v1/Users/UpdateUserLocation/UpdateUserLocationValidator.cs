namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUserLocation;

public class UpdateUserLocationValidator : Validator<UpdateUserLocationRequest>
{
    public UpdateUserLocationValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Accuracy).GreaterThan(0);
        RuleFor(x => x.Heading).GreaterThanOrEqualTo(0).LessThan(360);
        RuleFor(x => x.Speed).GreaterThanOrEqualTo(0);
    }
}
