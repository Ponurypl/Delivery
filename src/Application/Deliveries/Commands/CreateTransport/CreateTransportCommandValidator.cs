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

        RuleFor(x => x.TransportUnits).NotEmpty();
        RuleForEach(x => x.TransportUnits).ChildRules(x => {
            x.RuleFor(x => x.Barcode).NotEmpty().When(x => x.Amount is null || x.UnitOfMeasureId is null);

            x.RuleFor(x => x.Amount).GreaterThan(0).When(x => x.UnitOfMeasureId is not null && string.IsNullOrWhiteSpace(x.Barcode));
            x.RuleFor(x => x.Amount).Empty().When(x => x.Barcode is not null);

            x.RuleFor(x => x.UnitOfMeasureId).NotEmpty().When(x => x.Amount is not null && string.IsNullOrWhiteSpace(x.Barcode));
            x.RuleFor(x => x.UnitOfMeasureId).Empty().When(x => x.Barcode is not null);

            x.RuleFor(x => x.Description).NotEmpty();
            x.RuleFor(x => x.Number).NotEmpty();
            x.RuleFor(x => x.RecipientPhoneNumber).NotEmpty();
            x.RuleFor(x => x.RecipientStreetNumber).NotEmpty();
            x.RuleFor(x => x.RecipientTown).NotEmpty();
            x.RuleFor(x => x.RecipientCountry).NotEmpty();
            x.RuleFor(x => x.RecipientPostCode).NotEmpty();

            x.RuleFor(x => x.RecipientName).NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.RecipientLastName));
            x.RuleFor(x => x.RecipientLastName).NotEmpty().Unless(x => string.IsNullOrWhiteSpace(x.RecipientName));
            x.RuleFor(x => x.RecipientCompanyName).NotEmpty().When(x => string.IsNullOrWhiteSpace(x.RecipientLastName) 
                                                                            || string.IsNullOrWhiteSpace(x.RecipientName));
                
            });

        RuleFor(x => x.TotalWeight).GreaterThan(0).When(x => x.TotalWeight is not null);
    }
}
