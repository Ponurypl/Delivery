namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

public sealed class UpdateUserLocationCommandValidator : AbstractValidator<UpdateUserLocationCommand>
{
    public UpdateUserLocationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Accuracy).GreaterThan(0).PrecisionScale(3, 0);
        RuleFor(x => x.Heading).GreaterThanOrEqualTo(0).LessThan(360).PrecisionScale(5, 2);
        RuleFor(x => x.Speed).GreaterThanOrEqualTo(0).PrecisionScale(6, 2);
        RuleFor(x => x.Longitude).PrecisionScale(8, 5);
        RuleFor(x => x.Latitude).PrecisionScale(8, 5);
    }
}
