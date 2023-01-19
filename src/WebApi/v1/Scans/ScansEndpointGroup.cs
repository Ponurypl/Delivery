using MultiProject.Delivery.WebApi.Common.Auth;

namespace MultiProject.Delivery.WebApi.v1.Scans;

public sealed class ScansEndpointGroup : Group
{
    public ScansEndpointGroup()
    {
        Configure("scans", ep =>
                                {
                                    ep.Description(d =>
                                                   {
                                                       d.ProducesValidationProblem();
                                                   });
                                    ep.AuthSchemes(AuthConsts.AccessSchema);
                                });
    }
}