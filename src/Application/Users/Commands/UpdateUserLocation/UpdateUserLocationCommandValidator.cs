namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

public sealed class UpdateUserLocationCommandValidator : AbstractValidator<UpdateUserLocationCommand>
{
    public UpdateUserLocationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Accuracy).GreaterThan(0);
        RuleFor(x => x.Heading).GreaterThanOrEqualTo(0).LessThan(360);
        RuleFor(x => x.Speed).GreaterThanOrEqualTo(0);
    }
}
