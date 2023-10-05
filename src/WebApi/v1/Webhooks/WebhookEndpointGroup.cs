using MultiProject.Delivery.WebApi.Common.Auth;

namespace MultiProject.Delivery.WebApi.v1.Deliveries;

public sealed class WebhookEndpointGroup : Group
{
    public WebhookEndpointGroup()
    {

        Configure("Webhooks", ep =>
                                  {
                                      ep.Description(d =>
                                                         {
                                                             d.ProducesValidationProblem();
                                                         });
                                      ep.AuthSchemes(AuthConsts.AccessSchema);
                                  });
        
    }
}
