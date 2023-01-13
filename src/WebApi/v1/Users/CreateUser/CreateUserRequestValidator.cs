namespace MultiProject.Delivery.WebApi.v1.Users.CreateUser;

public sealed class CreateUserRequestValidator : Validator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(x => x.Username).MaximumLength(50);
        RuleFor(x => x.Password).MaximumLength(35); // TODO: kurde i w sumie skąd mam wiedzieć jaki jest dobry max, skoro mamy bcrypta po drodze? Trzeba przetestować testem jednostkowym
        RuleFor(x => x.PhoneNumber).Matches(@"^(\+\d{2,3})?\d{9}$").MaximumLength(15);
        RuleFor(x => x.Role).IsInEnum();
    }
    
}