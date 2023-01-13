using FluentValidation;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().NotEqual(x => x.Password).MaximumLength(50);
        RuleFor(x => x.Password).NotEmpty().MaximumLength(100);

        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(15);
        RuleFor(x => x.Role).NotEmpty().IsInEnum();
    }
}
