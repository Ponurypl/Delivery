using FluentAssertions;
using MultiProject.Delivery.Infrastructure.Cryptography;
using System.Security.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Tests.Unit;

public class CryptoServiceTests
{
    private readonly CryptoService _sut = new();


    [Fact]
    public void Encrypt_WhenValidDataProvided_ThenHashReturned()
    {
        //Arrange
        var plainText = "test";
        
        //Act
        string result = _sut.Encrypt(plainText);
        
        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().NotBe(plainText);
    }

    [Fact]
    public void Encrypt_WithPhrase_WhenValidDataProvided_ThenHashReturned()
    {
        //Arrange
        var plainText = "test";
        var phrase = "phrase";

        //Act
        string result = _sut.Encrypt(plainText, phrase);

        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().NotBe(plainText);
    }

    [Fact]
    public void Encrypt_WhenValidDataProvidedTwice_ThenReturnedHashesAreDifferent()
    {
        //Arrange
        var plainText = "test";

        //Act
        string hash1 = _sut.Encrypt(plainText);
        string hash2 = _sut.Encrypt(plainText);
        
        //Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void Encrypt_WithPhrase_WhenValidDataProvidedTwice_ThenReturnedHashesAreDifferent()
    {
        //Arrange
        var plainText = "test";
        var phrase = "phrase";

        //Act
        string hash1 = _sut.Encrypt(plainText, phrase);
        string hash2 = _sut.Encrypt(plainText, phrase);

        //Assert
        hash1.Should().NotBe(hash2);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Encrypt_WhenNullOrEmptyStringProvided_ThenExceptionIsThrown(string? plainText)
    {
        //Arrange
        var action = () => _sut.Encrypt(plainText);

        //Act

        //Assert
        action.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Decrypt_WhenValidDataProvided_ThenPlainTextReturned()
    {
        //Arrange
        var plainText = "test";
        string hash = _sut.Encrypt(plainText);
        
        //Act
        string result = _sut.Decrypt(hash);
        
        //Assert
        result.Should().Be(plainText);
    }

    [Fact]
    public void Decrypt_WithPhrase_WhenValidDataProvided_ThenPlainTextReturned()
    {
        //Arrange
        var plainText = "test";
        var phrase = "phrase";

        string hash = _sut.Encrypt(plainText, phrase);

        //Act
        string result = _sut.Decrypt(hash, phrase);

        //Assert
        result.Should().Be(plainText);
    }

    [Fact]
    public void Decrypt_WithPhrase_WhenInvalidPhraseProvided_ThenExceptionIsThrown()
    {
        //Arrange
        var plainText = "test";
        var phrase1 = "phras51561651561515e";
        var phrase2 = "15478";

        string hash = _sut.Encrypt(plainText, phrase1);
        var action = () => _sut.Decrypt(hash, phrase2);

        //Act

        //Assert
        action.Should().Throw<CryptographicException>();
    }




    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Decrypt_WhenNullOrEmptyStringProvided_ThenExceptionIsThrown(string? hash)
    {
        //Arrange
        var action = () => _sut.Decrypt(hash);
        //Act
        //Assert
        action.Should().Throw<ArgumentException>();
    }
}