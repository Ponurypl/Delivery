namespace MultiProject.Delivery.WebApi.v1.Auth;

public sealed class AuthEndpointGroup : Group
{
    public AuthEndpointGroup()
    {
        Configure("auth", _ => {});
    }
}