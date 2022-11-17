namespace MultiProject.Delivery.Application.Common.Cryptography;

public interface IHashService
{
    string Hash(string plainText);
    string Hash(string plainText, string salt);
}