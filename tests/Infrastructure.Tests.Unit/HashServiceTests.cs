using FluentAssertions;
using MultiProject.Delivery.Infrastructure.Cryptography;

namespace MultiProject.Delivery.Infrastructure.Tests.Unit;

public class HashServiceTests
{
    private readonly HashService _sut = new();

    [Fact]
    public void Hash_WhenValidDataProvided_ThenHashReturned()
    {
        //Arrange
        var plainText = "test";

        //Act
        string result = _sut.Hash(plainText);

        //Assert
        result.Should().NotBeNullOrEmpty();
        result.Should().NotBe(plainText);
    }

    [Fact]
    public void Hash_WhenValidDataProvidedTwice_ThenReturnedHashesAreDifferent()
    {
        //Arrange
        var plainText = "test";

        //Act
        string hash1 = _sut.Hash(plainText);
        string hash2 = _sut.Hash(plainText);

        //Assert
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void Hash_WhenInvalidDataProvided_ThenExceptionIsThrown()
    {
        //Arrange
        string? plainText = null;

        //Act
        Action act = () => _sut.Hash(plainText);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Verify_WhenValidDataProvided_ThenTrueReturned()
    {
        //Arrange
        var plainText = "test";
        string hash = _sut.Hash(plainText);

        //Act
        bool result = _sut.Verify(plainText, hash);

        //Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Verify_WhenInvalidHashProvided_ThenFalseReturned()
    {
        //Arrange
        var plainText1 = "test";
        var plainText2 = "test123";
        string hash = _sut.Hash(plainText1);

        //Act
        bool result = _sut.Verify(plainText2, hash);

        //Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Verify_WhenNullPlainTextProvided_ThenExceptionIsThrown()
    {
        //Arrange
        var plainText = "test";
        string hash = _sut.Hash(plainText);

        //Act
        Action act = () => _sut.Verify(null, hash);

        //Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Verify_WhenNullHashProvided_ThenExceptionIsThrown()
    {
        //Arrange
        var plainText = "test";

        //Act
        Action act = () => _sut.Verify(plainText, null);

        //Assert
        act.Should().Throw<ArgumentException>();
    }
}