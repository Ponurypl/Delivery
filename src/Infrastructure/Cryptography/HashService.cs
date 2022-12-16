using BCrypt.Net;
using MultiProject.Delivery.Application.Common.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Cryptography;

internal sealed class HashService : IHashService
{
    public string Hash(string plainText)
    {
        var salt = BCrypt.Net.BCrypt.GenerateSalt(13);
        return BCrypt.Net.BCrypt.HashPassword(plainText, salt, true, HashType.SHA512);
    }

    public bool Verify(string plainText, string password)
    {
        return BCrypt.Net.BCrypt.Verify(plainText, password, true, HashType.SHA512);
    }
}