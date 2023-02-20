using ErrorOr;
using MultiProject.Delivery.Domain.Tests.Unit.DataProviders;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit;

public class UserTests
{
    [Theory]
    [ClassData(typeof(UserCreateWrongData))]
    public void Create_WhenRequiredParametersAreNullOrEmpty_ThenFailureIsReturned(UserRole role, string username, string password, string phone)
    {
        //Arrange

        //Act
        var result = User.Create(role, username, password, phone);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        //result.FirstError.Should().Be(DomainFailures.Users.InvalidUser);
    }

    [Fact]
    public void Create_WhenValidDataProvided_ThenNewValidObjectCreated()
    {
        //Arrange
        var role = UserRole.Deliverer;
        var username = "abc";
        var password = "password@#$hash!!!";
        var phone = "1234567890";

        //Act
        var result = User.Create(role, username, password, phone);

        //Assert
        result.IsError.Should().BeFalse();

        var obj = result.Value;
        obj.Should().NotBeNull();
        obj.Role.Should().Be(role);
        obj.Username.Should().Be(username);
        obj.Password.Should().Be(password);
        obj.PhoneNumber.Should().Be(phone);
        obj.Id.Should().NotBe(UserId.Empty);
    }

    [Fact]
    public void Create_WhenTwoObjectsCreated_ThenIdsAreNotTheSame()
    {
        //Arrange
        var role = UserRole.Deliverer;
        var username = "abc";
        var password = "password@#$hash!!!";
        var phone = "1234567890";

        //Act
        var result = User.Create(role, username, password, phone);
        var result2 = User.Create(role, username, password, phone);

        //Assert
        result.Value.Id.Should().NotBe(result2.Value.Id);
    }
}