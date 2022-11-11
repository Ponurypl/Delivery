using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using System.Linq;

using FluentValidation;

namespace MultiProject.Delivery.Domain.Deliveries.Validators
{
    internal class TransportUnitValidator : AbstractValidator<NewTransportUnit>
    {
        public TransportUnitValidator()
        {
            //TODO: Usunąć powielenie komunikatu. Dane testowe - podany tylko Amount.
            var message = $"Transport unit not specified properly. You should specify Barcode or UnitOfMeasure together with Amount";

            When(x => x.UnitOfMeasureId is null || x.Amount is null, () =>
            {
                RuleFor(x => x.Barcode).NotEmpty().WithMessage(message);
            }).Otherwise(() =>
            {
                RuleFor(x => x.Barcode).Empty().WithMessage(message);
            });

            RuleFor(x => x.UnitOfMeasureId).NotEmpty().When(x => x.Amount is not null).WithMessage(message);
            RuleFor(x => x.Amount).GreaterThan(0).When(x => x.UnitOfMeasureId is not null);
        }
    }

    internal class TransportUnitCollectionValidator : AbstractValidator<List<NewTransportUnit>>
    {
        public TransportUnitCollectionValidator()
        {
            RuleForEach(x => x).SetValidator(new TransportUnitValidator());
        }
    }
}
