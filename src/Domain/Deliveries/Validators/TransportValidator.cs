using MultiProject.Delivery.Domain.Deliveries.Entities;

namespace MultiProject.Delivery.Domain.Deliveries.Validators;

public sealed class TransportValidator : AbstractValidator<Transport>
{
    public TransportValidator()
    {
        RuleFor(x => x.DelivererId).NotEmpty();
        RuleFor(x => x.ManagerId).NotEmpty();
        RuleFor(x => x.TransportUnits).NotEmpty();

    }
}
