namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateScan;

public sealed class ScanGeolocationValidator : AbstractValidator<ScanGelocation>
{
    public ScanGeolocationValidator()
    {
        RuleFor(x => x.Accuracy).GreaterThan(0);
    }
}
