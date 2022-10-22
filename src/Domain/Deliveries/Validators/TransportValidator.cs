using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Validators;

public sealed class TransportValidator : AbstractValidator<Transport>
{
    public TransportValidator()
    {
        RuleFor(x => x.Deliverer).NotEmpty();
        RuleFor(x => x.Deliverer.Role).Equal(Users.Enums.UserRole.Deliverer);
        RuleFor(x => x.Manager).NotEmpty();
        RuleFor(x => x.Manager.Role).Equal(Users.Enums.UserRole.Manager);
        RuleFor(x => x.TransportUnits).NotEmpty();

    }
}
