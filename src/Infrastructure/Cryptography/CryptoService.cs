using MultiProject.Delivery.Application.Common.Cryptography;
using System.Security.Cryptography;
using System.Text;

namespace MultiProject.Delivery.Infrastructure.Cryptography;

internal sealed class CryptoService : ICryptoService
{
    private const string _passPhrase = "XqV0l75s1i%z1$Tbcned@%ajCA8^BTWX";
    private const int _keySize = 256;
    private const int _blockSize = 128;
    private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA512;

    private const int _keyBytes = _keySize / 8;
    private const int _blockBytes = _blockSize / 8;
    private const int _derivationIterations = 1000;

    private readonly CipherMode _cipherMode = CipherMode.CBC;
    private readonly PaddingMode _paddingMode = PaddingMode.PKCS7;

    public string Encrypt(string plainText) => Encrypt(plainText, _passPhrase);

    public string Encrypt(string plainText, string passPhrase)
    {
        ArgumentException.ThrowIfNullOrEmpty(plainText);
        ArgumentException.ThrowIfNullOrEmpty(passPhrase);

        var saltStringBytes = RandomNumberGenerator.GetBytes(_keyBytes);
        var ivStringBytes = RandomNumberGenerator.GetBytes(_blockBytes);

        var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        using var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, _derivationIterations, _hashAlgorithm);
        var keyBytes = password.GetBytes(_keyBytes);

        using var symmetricKey = Aes.Create();
        symmetricKey.BlockSize = _blockSize;
        symmetricKey.KeySize = _keySize;
        symmetricKey.Padding = _paddingMode;
        symmetricKey.Mode = _cipherMode;

        using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
        using var memoryStream = new MemoryStream();
        using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();

        var cipherTextBytes = saltStringBytes;
        cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
        cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();

        return Convert.ToBase64String(cipherTextBytes);
    }

    public string Decrypt(string hashedValue) => Decrypt(hashedValue, _passPhrase);

    public string Decrypt(string hashedValue, string passPhrase)
    {
        ArgumentException.ThrowIfNullOrEmpty(hashedValue);
        ArgumentException.ThrowIfNullOrEmpty(passPhrase);

        var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(hashedValue);
        var saltStringBytes = cipherTextBytesWithSaltAndIv[.._keyBytes];
        var ivStringBytes = cipherTextBytesWithSaltAndIv[_keyBytes..(_keyBytes + _blockBytes)];
        var cipherTextBytes = cipherTextBytesWithSaltAndIv[(_keyBytes + _blockBytes)..];

        using var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, _derivationIterations, _hashAlgorithm);
        var keyBytes = password.GetBytes(_keyBytes);

        using var symmetricKey = Aes.Create();
        symmetricKey.Padding = _paddingMode;
        symmetricKey.Mode = _cipherMode;

        using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
        using var memoryStream = new MemoryStream(cipherTextBytes);
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

        var plainTextBytes = new byte[cipherTextBytes.Length];
        Span<byte> buffer = new(plainTextBytes);

        int decryptedByteCount = 0;
        while (decryptedByteCount < buffer.Length)
        {
            int bytesRead = cryptoStream.Read(buffer.Slice(decryptedByteCount));
            if (bytesRead == 0) break;
            decryptedByteCount += bytesRead;
        }

        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
}