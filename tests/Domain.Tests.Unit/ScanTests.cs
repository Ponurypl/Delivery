using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.Enums;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Data;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit;
public class ScanTests
{

    [Fact]
    public void Create_WhenValidDataProvided_ThenNewValidObjectReturned()
    {
        //Arrange
        DateTime creationDate = new(2023, 1, 1, 12, 0, 0);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetId();
        UserId userId = DomainFixture.Users.GetId();

        //Act
        ErrorOr<Scan> result = Scan.Create(transportUnitId, userId, dateTimeProvider);

        //Assert
        result.IsError.Should().BeFalse();

        Scan obj = result.Value;
        obj.Should().NotBeNull();
        obj.TransportUnitId.Should().Be(transportUnitId);
        obj.DelivererId.Should().Be(userId);
        obj.Id.Should().Be(ScanId.Empty);
        obj.LastUpdateDate.Should().Be(creationDate);
        obj.Status.Should().Be(ScanStatus.Valid);
        obj.Location.Should().BeNull();
        obj.Quantity.Should().BeNull();
    }

    [Fact]
    public void Create_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        TransportUnitId transportUnitId = DomainFixture.TransportUnits.GetId();
        UserId userId = DomainFixture.Users.GetId();

        //Act
        ErrorOr<Scan> result = Scan.Create(transportUnitId, userId, null!);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Theory]
    [MemberData(nameof(ScanTestsData.Create_InvalidData), MemberType = typeof(ScanTestsData))]
    public void Create_WhenInvalidDataProvided_ThenFailureReturned(int intTransportUnitId, UserId userId)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(intTransportUnitId);

        //Act
        ErrorOr<Scan> result = Scan.Create(transportUnitId, userId, dateTimeProvider);
        
        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Scans.InvalidScan);
    }

    [Theory]
    [InlineData(12, 43, 2)]
    [InlineData(12.12312, 43.23, 0.3424)]
    [InlineData(-123, 76554, 453)]
    [InlineData(123, -7644, 1)]
    public void SetGeolocation_WhenValidDataProvided_ThenScanIsCreated(double latitude, double longitude, double accuracy)
    {
        //Arrange
        Scan sut = DomainFixture.Scans.GetScan();

        //Act
        ErrorOr<Updated> result = sut.SetGeolocation(latitude, longitude, accuracy);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Updated);
        sut.Location.Should().NotBeNull();
        sut.Location!.Latitude.Should().Be(latitude);
        sut.Location!.Longitude.Should().Be(longitude);
        sut.Location!.Accuracy.Should().Be(accuracy);
    }

    [Theory]
    [InlineData(12, 43, 0)]
    [InlineData(12, 43, -0.1)]
    public void SetGeolocation_WhenInValidDataProvided_ThenFailureIsReturned(double latitude, double longitude, double accuracy)
    {
        //Arrange
        Scan sut = DomainFixture.Scans.GetScan();

        //Act
        ErrorOr<Updated> result = sut.SetGeolocation(latitude, longitude, accuracy);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Geolocations.InvalidGeolocation);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(1.342)]
    [InlineData(135353)]
    [InlineData(0.431)]
    public void SetQuantity_WhenValidDataProvided_ThenScanQuantityIsUpdated(double quantity)
    {
        //Arrange
        Scan sut = DomainFixture.Scans.GetScan();

        //Act
        ErrorOr<Updated> result = sut.SetQuantity(quantity);

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.Quantity.Should().Be(quantity);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-34)]
    [InlineData(-34.3543)]
    [InlineData(-0.12)]
    public void SetQuantity_WhenInvalidDataProvided_ThenFailureIsReturned(double quantity)
    {
        //Arrange
        Scan sut = DomainFixture.Scans.GetScan();

        //Act
        ErrorOr<Updated> result = sut.SetQuantity(quantity);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Scans.InvalidScan);
    }
}
