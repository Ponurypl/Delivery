namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public sealed class GetTrasnportDetailsValidator : Validator<GetTransportDetailsRequest>
{
    public GetTrasnportDetailsValidator()
    {
        RuleFor(x => x.TransportId).NotEmpty();
    }
}
