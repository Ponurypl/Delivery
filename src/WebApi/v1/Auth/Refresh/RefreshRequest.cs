using System.Net;

namespace MultiProject.Delivery.WebApi.v1.Auth.Refresh;

public sealed record RefreshRequest
{
    [FromHeader(nameof(HttpRequestHeader.Authorization))]
    public string RefreshToken { get; init; } = "";
}