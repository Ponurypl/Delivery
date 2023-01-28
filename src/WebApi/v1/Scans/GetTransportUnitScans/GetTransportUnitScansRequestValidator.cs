namespace MultiProject.Delivery.WebApi.v1.Scans.GetTransportUnitScans;

public sealed class GetTransportUnitScansRequestValidator : Validator<GetTransportUnitScansRequest>
{
    public GetTransportUnitScansRequestValidator()
    {
        RuleFor(x => x.TransportUnitId).NotEmpty();
    }
}
