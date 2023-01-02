using FastEndpoints.Security;
using Microsoft.Extensions.Caching.Distributed;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.WebApi.Common.Auth;
using MultiProject.Delivery.WebApi.v1.Auth.Common;
using System.Security.Claims;

namespace MultiProject.Delivery.WebApi.v1.Auth.Services;

internal sealed class TokenService : ITokenService
{
    private readonly IDistributedCache _cache;
    private readonly IDateTime _dateTime;

    public TokenService(IDistributedCache cache, IDateTime dateTime)
    {
        _cache = cache;
        _dateTime = dateTime;
    }

    public async Task<(TokenDetails Access, TokenDetails Refresh)> GetNewTokensAsync(string username, Guid userId, CancellationToken ct = default)
    {
        var accessExpire = _dateTime.UtcNow.AddMinutes(10);
        var accessToken = JWTBearer.CreateToken(signingKey: AuthConsts.JwtSigningKey,
                                                expireAt: accessExpire,
                                                audience: AuthConsts.AccessSchema,
                                                claims: new Claim[] { new("Username", username),
                                                                        new("UserId", userId.ToString()) });

        var refreshExpire = _dateTime.UtcNow.AddHours(2);
        var refreshToken = JWTBearer.CreateToken(signingKey: AuthConsts.JwtSigningKey,
                                                 expireAt: refreshExpire,
                                                 audience: AuthConsts.RefreshSchema,
                                                 claims: new Claim[] { new("Username", username),
                                                                         new("UserId", userId.ToString()) });

        await _cache.SetAsync($"RefreshToken:{refreshToken}", userId.ToByteArray(),
                              new DistributedCacheEntryOptions { AbsoluteExpiration = refreshExpire.AddMinutes(2) }, ct);

        return (new TokenDetails { ExpireAt = accessExpire, Token = accessToken },
                new TokenDetails { ExpireAt = refreshExpire, Token = refreshToken });
    }

    public async Task<bool> ExistsAsync(string token, CancellationToken ct = default)
    {
        return await _cache.GetAsync($"RefreshToken:{token}", ct) is not null;
    }


    public async Task RemoveTokenAsync(string token, CancellationToken ct = default)
    {
        await _cache.RemoveAsync($"RefreshToken:{token}", ct);
    }
}