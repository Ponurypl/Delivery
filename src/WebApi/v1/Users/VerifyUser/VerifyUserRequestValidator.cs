namespace MultiProject.Delivery.WebApi.v1.Users.VerifyUser;

public sealed class VerifyUserRequestValidator : Validator<VerifyUserRequest>
{
    public VerifyUserRequestValidator()
    {
        RuleFor(x => x.Username).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}
