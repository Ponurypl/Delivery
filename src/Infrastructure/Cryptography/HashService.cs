using BCrypt.Net;
using MultiProject.Delivery.Application.Common.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Cryptography;

internal sealed class HashService : IHashService
{
    private const string _salt = "3^5o0OmW&PB@kD8PEHemDU3aTV%D@!Eh";

    public string Hash(string plainText)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainText, _salt, true, HashType.SHA512);
    }

    public bool Verify(string plainText, string password)
    {
        return BCrypt.Net.BCrypt.Verify(plainText, password, true, HashType.SHA512);
    }
}