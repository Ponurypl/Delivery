using FluentValidation;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Domain.Users.Validators;

public sealed  class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Role).IsInEnum();
        RuleFor(x => x.Password).NotEqual(x => x.Login);
        //TODO : sprawdzenie czy numer telefonu jest możliwy
    }
}
