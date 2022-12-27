namespace MultiProject.Delivery.WebApi.v1.Auth.Login;

public record TokenDetails
{
    public string Token { get; init; } = default!;
    public DateTime ExpireAt { get; init; }
}