namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUserLocation;

public class UpdateUserLocationValidator : Validator<UpdateUserLocationRequest>
{
    public UpdateUserLocationValidator()
    {
        RuleFor(x => x.Accuracy).GreaterThan(0).PrecisionScale(3,0);
        RuleFor(x => x.Heading).GreaterThanOrEqualTo(0).LessThan(360).PrecisionScale(5,2);
        RuleFor(x => x.Speed).GreaterThanOrEqualTo(0).PrecisionScale(6,2);
        RuleFor(x => x.Latitude).PrecisionScale(8, 5); 
        RuleFor(x => x.Longitude).PrecisionScale(8, 5);
    }
}
