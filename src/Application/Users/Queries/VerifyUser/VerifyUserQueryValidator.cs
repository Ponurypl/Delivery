using FluentValidation;

namespace MultiProject.Delivery.Application.Users.Queries.VerifyUser;
public sealed class VerifyUserQueryValidator : AbstractValidator<VerifyUserQuery>
{
    public VerifyUserQueryValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();    
    }
}
