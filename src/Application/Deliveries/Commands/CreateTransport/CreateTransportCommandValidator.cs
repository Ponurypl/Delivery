using FluentValidation;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;
public sealed class CreateTransportCommandValidator : AbstractValidator<CreateTransportCommand>
{
    public CreateTransportCommandValidator()
    {

        RuleFor(x => x.DelivererId).NotEmpty();
        RuleFor(x => x.ManagerId).NotEmpty();
        RuleFor(x => x.StartDate).NotEmpty();
        RuleFor(x => x.Number).NotEmpty();

        RuleFor(x => x.TransportUnits).NotEmpty().WithMessage("At least One TransportUnit must be specified in delivery");
        RuleForEach(x => x.TransportUnits).SetValidator(new TransportUnitToCreateValidator());

        RuleFor(x => x.TotalWeight).GreaterThan(0).When(x => x.TotalWeight is not null);
    }
}
