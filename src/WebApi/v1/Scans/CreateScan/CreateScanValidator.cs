namespace MultiProject.Delivery.WebApi.v1.Scans.CreateScan;

public sealed class CreateScanValidator : Validator<CreateScanRequest>
{
    public CreateScanValidator()
    {
        RuleFor(x => x.DelivererId).NotEmpty();
        RuleFor(x => x.TransportId).NotEmpty();
        RuleFor(x => x.TransportUnitId).NotEmpty();
        RuleFor(x => x.Quantity).PrecisionScale(8, 3);
        RuleFor(x => x.Location).SetValidator(new RequestScanGeolocationValidator()!).When(x => x.Location is not null);
    }
}
