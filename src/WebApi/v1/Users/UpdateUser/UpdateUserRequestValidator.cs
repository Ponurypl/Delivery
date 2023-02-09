namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUser;

public class UpdateUserRequestValidator : Validator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.IsActive).NotEmpty();
        RuleFor(x => x.PhoneNumber).Matches(@"^(\+\d{2,3})?\d{9}$").MaximumLength(15);
        RuleFor(x => x.Role).IsInEnum().NotEmpty();
    }
}
