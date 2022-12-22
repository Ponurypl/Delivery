namespace MultiProject.Delivery.WebApi.v1.Dictionaries;

public sealed class DictionaryEndpointGroup : Group
{
    public DictionaryEndpointGroup()
    {
        Configure("Dictionaries", ep =>
                                  {
                                      ep.Description(d =>
                                                     {
                                                         d.Produces(StatusCodes.Status401Unauthorized);
                                                         d.ProducesValidationProblem();
                                                         d.AllowAnonymous();
                                                     });
                                  });
    }
}
