namespace MultiProject.Delivery.WebApi.v1.Scans.GetScan;

public sealed class GetScanRequestValidator : Validator<GetScanRequest>
{
    public GetScanRequestValidator()
    {
        RuleFor(x => x.ScanId).NotEmpty();
    }
}
