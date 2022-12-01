using FluentValidation;

namespace MultiProject.Delivery.Application.Users.Queries.GetUser;
public sealed class GetUserQueryVallidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryVallidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}
