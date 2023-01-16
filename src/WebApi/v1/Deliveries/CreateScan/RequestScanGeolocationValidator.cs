namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateScan;

public sealed class RequestScanGeolocationValidator : Validator<RequestScanGeolocation?>
{
    public RequestScanGeolocationValidator()
    {
        RuleFor(x => x.Accuracy).GreaterThan(0).PrecisionScale(3, 0);
        RuleFor(x => x.Latitude).NotEmpty().PrecisionScale(8, 5);
        RuleFor(x => x.Longitude).NotEmpty().PrecisionScale(8, 5);
    }
}
