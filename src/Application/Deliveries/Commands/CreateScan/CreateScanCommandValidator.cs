using FluentValidation;

namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;
public sealed class CreateScanCommandValidator : AbstractValidator<CreateScanCommand>
{
    public CreateScanCommandValidator()
    {
        RuleFor(x => x.LocationAccuracy).NotEmpty().GreaterThan(0).When(x => x.LocationLatitude is not null || x.LocationLongitude is not null);
        RuleFor(x => x.LocationLatitude).NotEmpty().When(x => x.LocationAccuracy is not null || x.LocationLongitude is not null);
        RuleFor(x => x.LocationLongitude).NotEmpty().When(x => x.LocationLatitude is not null || x.LocationAccuracy is not null);
    }
}
