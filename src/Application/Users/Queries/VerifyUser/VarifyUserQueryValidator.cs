using FluentValidation;

namespace MultiProject.Delivery.Application.Users.Queries.VerifyUser;
public sealed class VarifyUserQueryValidator : AbstractValidator<VerifyUserQuery>
{
    public VarifyUserQueryValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();    
    }
}
