using MultiProject.Delivery.WebApi.Common.Auth;

namespace MultiProject.Delivery.WebApi.v1.Dictionaries;

public sealed class DictionaryEndpointGroup : Group
{
    public DictionaryEndpointGroup()
    {
        Configure("dictionaries", ep =>
                                  {
                                      ep.Description(d =>
                                                     {
                                                         d.ProducesValidationProblem();
                                                     });
                                      ep.AuthSchemes(AuthConsts.AccessSchema);
                                  });
    }
}
