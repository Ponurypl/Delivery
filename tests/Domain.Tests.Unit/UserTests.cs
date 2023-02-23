using ErrorOr;
using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
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
        result.FirstError.Should().Be(DomainFailures.Users.InvalidUser);
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

    [Theory]
    [InlineData(15, 200, 1, 200, 100, "2023-01-01")]
    [InlineData(-200, -1500, 150, 0, 15, "2023-02-15")]
    public void UpdateGeolocation_WhenValidDataProvided_ThenLocationIsCreated(
        double latitude, double longitude, double accuracy, double heading,
        double speed, string readDateTime)
    {
        //Arrange
        var sut = User.Create(UserRole.Deliverer, "abc", "password@#$hash!!!", "1234567890").Value;
        var castedDateTime = DateTime.Parse(readDateTime);

        //Act
        var result = sut.UpdateGeolocation(latitude, longitude, accuracy, heading,
                                           speed, castedDateTime);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Updated);
        sut.Location.Should().NotBeNull();
        sut.Location!.Latitude.Should().Be(latitude);
        sut.Location!.Longitude.Should().Be(longitude);
        sut.Location!.Accuracy.Should().Be(accuracy);
        sut.Location!.Heading.Should().Be(heading);
        sut.Location!.Speed.Should().Be(speed);
        sut.Location!.LastUpdateDate.Should().Be(castedDateTime);
    }


    [Theory]
    [InlineData(15, 200, 0, 10, 100, "2023-01-01")]
    [InlineData(15, 200, -15, 10, 100, "2023-01-01")]
    [InlineData(15, 200, 10, -15, 100, "2023-01-01")]
    [InlineData(15, 200, 10, 15, -15, "2023-01-01")]
    public void UpdateGeolocation_WhenInvalidDataProvided_ThenErrorIsReturned(
        double latitude, double longitude, double accuracy, double heading,
        double speed, string readDateTime)
    {
        //Arrange
        var sut = User.Create(UserRole.Deliverer, "abc", "password@#$hash!!!", "1234567890").Value;
        var castedDateTime = DateTime.Parse(readDateTime);

        //Act
        var result = sut.UpdateGeolocation(latitude, longitude, accuracy, heading,
                                           speed, castedDateTime);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Geolocations.InvalidAdvancedGeolocation);
    }


    [Theory]
    [InlineData(15, 200, 1, 200, 100, "2023-01-01")]
    [InlineData(-200, -1500, 150, 0, 15, "2023-02-15")]
    public void UpdateGeolocation_WhenValidDataProvidedAndLocationIsNotNull_ThenLocationIsReplaced(
        double latitude, double longitude, double accuracy, double heading,
        double speed, string readDateTime)
    {
        //Arrange
        var sut = User.Create(UserRole.Deliverer, "abc", "password@#$hash!!!", "1234567890").Value;
        var castedDateTime = DateTime.Parse(readDateTime);
        sut.UpdateGeolocation(1, 1, 1, 1, 1, castedDateTime.AddDays(-1));

        //Act
        var result = sut.UpdateGeolocation(latitude, longitude, accuracy, heading,
                                           speed, castedDateTime);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Updated);
        sut.Location.Should().NotBeNull();
        sut.Location!.Latitude.Should().Be(latitude);
        sut.Location!.Longitude.Should().Be(longitude);
        sut.Location!.Accuracy.Should().Be(accuracy);
        sut.Location!.Heading.Should().Be(heading);
        sut.Location!.Speed.Should().Be(speed);
        sut.Location!.LastUpdateDate.Should().Be(castedDateTime);
    }

    [Theory]
    [InlineData(UserRole.Deliverer)]
    [InlineData(UserRole.Manager | UserRole.Deliverer)]
    public void CheckIfUserIsDeliverer_WhenUserIsDeliverer_ThenReturnSuccess(UserRole role)
    {
        //Arrange
        var sut = User.Create(role, "abc", "password@#$hash!!!", "1234567890").Value;

        //Act
        var result = sut.CheckIfUserIsDeliverer();

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Success);
    }

    [Theory]
    [InlineData(UserRole.Manager)]
    public void CheckIfUserIsDeliverer_WhenUserIsNotDeliverer_ThenReturnFailure(UserRole role)
    {
        //Arrange
        var sut = User.Create(role, "abc", "password@#$hash!!!", "1234567890").Value;

        //Act
        var result = sut.CheckIfUserIsDeliverer();

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Conflict);
        result.FirstError.Should().Be(DomainFailures.Users.UserDoesNotMeetRole);
    }
    
    [Theory]
    [InlineData(UserRole.Manager)]
    [InlineData(UserRole.Manager | UserRole.Deliverer)]
    public void CheckIfUserIsManager_WhenUserIsManager_ThenReturnSuccess(UserRole role)
    {
        //Arrange
        var sut = User.Create(role, "abc", "password@#$hash!!!", "1234567890").Value;

        //Act
        var result = sut.CheckIfUserIsManager();

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Success);
    }

    [Theory]
    [InlineData(UserRole.Deliverer)]
    public void CheckIfUserIsManager_WhenUserIsNotManager_ThenReturnFailure(UserRole role)
    {
        //Arrange
        var sut = User.Create(role, "abc", "password@#$hash!!!", "1234567890").Value;

        //Act
        var result = sut.CheckIfUserIsManager();

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Conflict);
        result.FirstError.Should().Be(DomainFailures.Users.UserDoesNotMeetRole);
    }
}