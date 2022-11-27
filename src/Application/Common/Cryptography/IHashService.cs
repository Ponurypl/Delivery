namespace MultiProject.Delivery.Application.Common.Cryptography;

public interface IHashService
{
    string Hash(string plainText);
    bool Verify(string plainText, string password);
}