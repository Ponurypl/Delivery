using MultiProject.Delivery.WebApi.v1.Auth.Common;

namespace MultiProject.Delivery.WebApi.v1.Auth.Services;

public interface ITokenService
{
    Task<(TokenDetails Access, TokenDetails Refresh)> GetNewTokensAsync(string username, Guid userId, CancellationToken ct = default);
    Task<bool> ExistsAsync(string token, CancellationToken ct = default);
    Task RemoveTokenAsync(string token, CancellationToken ct = default);
}