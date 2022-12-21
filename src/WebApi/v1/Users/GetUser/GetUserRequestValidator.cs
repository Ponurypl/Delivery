namespace MultiProject.Delivery.WebApi.v1.Users.GetUser;

public sealed class GetUserRequestValidator : Validator<GetUserRequest>
{
    public GetUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}