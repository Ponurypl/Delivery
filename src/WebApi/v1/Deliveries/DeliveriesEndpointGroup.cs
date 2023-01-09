using MultiProject.Delivery.WebApi.Common.Auth;

namespace MultiProject.Delivery.WebApi.v1.Deliveries;

public class DeliveriesEndpointGroup : Group
{
    public DeliveriesEndpointGroup()
    {

        Configure("deliveries", ep =>
                                  {
                                      ep.Description(d =>
                                                         {
                                                             d.ProducesValidationProblem();
                                                         });
                                      ep.AuthSchemes(AuthConsts.AccessSchema);
                                  });
        
    }
}
