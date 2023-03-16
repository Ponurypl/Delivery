namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().NotEqual(x => x.Password).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(35);

        RuleFor(x => x.PhoneNumber).NotEmpty().Matches(@"^(\+\d{2,3})?\d{9}$").MaximumLength(15);
        RuleFor(x => x.Role).NotEmpty().IsInEnum();
    }
}
