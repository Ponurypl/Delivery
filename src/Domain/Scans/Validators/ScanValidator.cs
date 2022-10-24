using FluentValidation;
using MultiProject.Delivery.Domain.Common.Extensions;
using MultiProject.Delivery.Domain.Common.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;

namespace MultiProject.Delivery.Domain.Scans.Validators;

internal class ScanValidator : AbstractValidator<Scan>
{
    public ScanValidator()
    {
        RuleFor(x => x.TransportUnit).NotEmpty();
        RuleFor(x => x.Deliverer).NotEmpty();

        When(x => x.Geolocalization is not null, () =>
        {
            RuleFor(x => x.Geolocalization!.Latitude).NotEmpty();
            RuleFor(x => x.Geolocalization!.Longitude).NotEmpty();
            RuleFor(x => x.Geolocalization!.Accuracy).NotEmpty();
        });
    }
}
