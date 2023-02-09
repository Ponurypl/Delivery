namespace MultiProject.Delivery.Application.Users.Commands.UpdateUser;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.PhoneNumber).Matches(@"^(\+\d{2,3})?\d{9}$").MaximumLength(15);
        RuleFor(x => x.Role).IsInEnum().NotEmpty();
    }
}
