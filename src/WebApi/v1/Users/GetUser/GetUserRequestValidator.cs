namespace MultiProject.Delivery.WebApi.v1.Users.GetUser;

public class GetUserRequestValidator : AbstractValidator<GetUserRequest>
{
    public GetUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}