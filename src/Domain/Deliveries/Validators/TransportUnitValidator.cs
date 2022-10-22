using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Common.Extensions;

namespace MultiProject.Delivery.Domain.Deliveries.Validators
{
    internal class TransportUnitValidator : AbstractValidator<NewTransportUnit>
    {
        public TransportUnitValidator()
        {
            //TODO: Aktualnie może sypnąć tym samym błędem X razy, a alternatywa jest... nieczytelna
            RuleFor(x => x).Must(x => string.IsNullOrWhiteSpace(x.Barcode) && (x.Amount is null || x.UnitOfMeasureId is null || x.Amount == 0 || x.UnitOfMeasureId == 0))
                           .WithMessage($"Transport unit not specified properly. You should specify Barcode or UnitOfMeasure together with Amount");
            RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x.Barcode) && (x.Amount is not null || x.UnitOfMeasureId is not null))
                           .WithMessage($"Transport unit not specified properly. You should specify Barcode or UnitOfMeasure together with Amount");
            RuleFor(x => x.Amount).NotEmpty().When(x => x.UnitOfMeasureId is null)
                           .WithMessage($"Transport unit not specified properly. You should specify Barcode or UnitOfMeasure together with Amount");
            RuleFor(x => x.UnitOfMeasureId).NotEmpty().When(x => x.UnitOfMeasureId is null)
                           .WithMessage($"Transport unit not specified properly. You should specify Barcode or UnitOfMeasure together with Amount");

            /* Alternatywa?

            RuleFor(x => x).Must(x => (string.IsNullOrWhiteSpace(x.Barcode) 
                                       && (x.Amount is null || x.UnitOfMeasureId is null || x.Amount == 0 || x.UnitOfMeasureId == 0))
                                  && (!string.IsNullOrWhiteSpace(x.Barcode) && (x.Amount is not null || x.UnitOfMeasureId is not null))
                                  && ((x.Amount != 0 && x.Amount is not null) || (x.UnitOfMeasureId is not null && x.UnitOfMeasureId != 0)))
                           .WithMessage($"Transport unit not specified properly. You should specify Barcode or UnitOfMeasure together with Amount");
            */

        }
    }
}
