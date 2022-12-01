using FluentValidation;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Username).NotEmpty().NotEqual(x => x.Password);
        RuleFor(x => x.Password).NotEmpty();

        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.Role).NotEmpty().IsInEnum();
    }
}
