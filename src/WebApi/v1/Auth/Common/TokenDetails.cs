namespace MultiProject.Delivery.WebApi.v1.Auth.Common;

public record TokenDetails
{
    public string Token { get; init; } = default!;
    public DateTime ExpireAt { get; init; }
}