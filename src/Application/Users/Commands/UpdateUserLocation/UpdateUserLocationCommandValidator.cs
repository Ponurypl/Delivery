using FluentValidation;

namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;
internal class UpdateUserLocationCommandValidator : AbstractValidator<UpdateUserLocationCommand>
{
    public UpdateUserLocationCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.Accuracy).GreaterThan(0);
    }
}
