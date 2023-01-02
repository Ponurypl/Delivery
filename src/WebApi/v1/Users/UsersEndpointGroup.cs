using MultiProject.Delivery.WebApi.Common.Auth;

namespace MultiProject.Delivery.WebApi.v1.Users;

public sealed class UsersEndpointGroup : Group
{
    public UsersEndpointGroup()
    {
        Configure("users", ep =>
                           {
                               ep.Description(b =>
                                              {
                                                  b.ProducesValidationProblem();
                                              });
                               ep.AuthSchemes(AuthConsts.AccessSchema);
                           });
    }
    
}