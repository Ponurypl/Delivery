namespace MultiProject.Delivery.Application.Common.Cryptography;

public interface ICryptoService
{
    string Encrypt(string plainText);
    string Decrypt(string hashString);
}