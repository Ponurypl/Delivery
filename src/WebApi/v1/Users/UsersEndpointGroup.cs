namespace MultiProject.Delivery.WebApi.v1.Users;

public sealed class UsersEndpointGroup : Group
{
    public UsersEndpointGroup()
    {
        Configure("users", ep =>
                           {
                               ep.Description(b =>
                                              {
                                                  b.Produces(StatusCodes.Status401Unauthorized);
                                                  b.ProducesValidationProblem();
                                                  b.AllowAnonymous();
                                              });
                           });
    }
    
}