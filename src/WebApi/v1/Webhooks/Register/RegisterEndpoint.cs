                                         using MultiProject.Delivery.WebApi.v1.Deliveries;

namespace MultiProject.Delivery.WebApi.v1.Webhooks.Register;

public sealed class RegisterEndpoint : Endpoint<RegisterRequest>
{
    public override void Configure()
    {
        Post("");
        Group<WebhookEndpointGroup>();
        Description(d =>
                    {
                        d.Produces(StatusCodes.Status404NotFound);

                    });
        Version(1);
    }

    public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
    {
        await SendOkAsync(ct);
    }
}