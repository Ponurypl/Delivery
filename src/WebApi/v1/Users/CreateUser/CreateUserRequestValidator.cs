namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

public sealed class CreateUserRequestValidator : Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.PhoneNumber).Matches(@"^(\+\d{2,3})?\d{9}$");
        RuleFor(x => x.Role).IsInEnum();
    }
    
}