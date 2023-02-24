using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit;
public class ScanTests
{

    [Theory]
    [InlineData(1, "0f0f1d91-37a5-4957-98fd-6e21f676be64")]
    [InlineData(1342, "80f77817-5a3d-4e1d-b203-6210fae49bf3")]
    public void Create_WhenValidDataProvided_ThenNewValidObjectReturned(int intTransportUnitId, Guid guidDelivererId)
    {
        //Arrange
        IDateTime DateTimeProviderSubsititute = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(intTransportUnitId);
        UserId userId = new(guidDelivererId);

        //Act
        ErrorOr<Scan> result = Scan.Create(transportUnitId, userId, DateTimeProviderSubsititute);
        Scan sut = result.Value;

        //Assert
        result.IsError.Should().BeFalse();
        sut.Should().NotBeNull();
        sut.TransportUnitId.Should().Be(transportUnitId);
        sut.DelivererId.Should().Be(userId);
        sut.Id.Should().Be(ScanId.Empty);
    }

    [Fact]
    public void Create_WhenDependencyNotProvided_ThenUnexpectedFailureReturned()
    {
        //Arrange
        TransportUnitId transportUnitId = new(1);
        UserId userId = new(Guid.Parse("80f77817-5a3d-4e1d-b203-6210fae49bf3"));

        //Act
        ErrorOr<Scan> sut = Scan.Create(transportUnitId, userId, null!);

        //Assert

        sut.IsError.Should().BeTrue();
        sut.FirstError.Type.Should().Be(ErrorType.Unexpected);
        sut.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Theory]
    [InlineData(1, "00000000-0000-0000-0000-000000000000")]
    [InlineData(0, "80f77817-5a3d-4e1d-b203-6210fae49bf3")]
    public void Create_WhenInvalidDataProvided_ThenFailureReturned(int intTransportUnitId, Guid guidDelivererId)
    {
        //Arrange
        IDateTime DateTimeProviderSubsititute = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(intTransportUnitId);
        UserId userId = new(guidDelivererId);

        //Act
        ErrorOr<Scan> sut = Scan.Create(transportUnitId, userId, DateTimeProviderSubsititute);

        //Assert

        sut.IsError.Should().BeTrue();
        sut.FirstError.Type.Should().Be(ErrorType.Validation);
        sut.FirstError.Should().Be(DomainFailures.Scans.InvalidScan);
    }

    [Theory]
    [InlineData(12, 43, 2)]
    [InlineData(12.12312, 43.23, 0.3424)]
    [InlineData(-123, 76554, 453)]
    [InlineData(123, -7644, 1)]
    public void AddGeolocation_WhenValidDataProvided_ThenScanIsCreated(double latitude, double longitude, double accuracy)
    {
        //Arrange
        IDateTime DateTimeProviderSubsititute = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(1);
        UserId userId = new(Guid.NewGuid());
        Scan sut = Scan.Create(transportUnitId, userId, DateTimeProviderSubsititute).Value;

        //Act
        ErrorOr<Updated> result = sut.AddGeolocation(latitude, longitude, accuracy);

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
    public void AddGeolocation_WhenInValidDataProvided_ThenFailureIsReturned(double latitude, double longitude, double accuracy)
    {
        //Arrange
        IDateTime DateTimeProviderSubsititute = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(1);
        UserId userId = new(Guid.NewGuid());
        Scan sut = Scan.Create(transportUnitId, userId, DateTimeProviderSubsititute).Value;

        //Act
        ErrorOr<Updated> result = sut.AddGeolocation(latitude, longitude, accuracy);

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
    public void AddQuantity_WhenValidDataProvided_ThenScanQuantityIsUpdated(double quantity)
    {
        //Arrange
        IDateTime DateTimeProviderSubsititute = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(1);
        UserId userId = new(Guid.NewGuid());
        Scan sut = Scan.Create(transportUnitId, userId, DateTimeProviderSubsititute).Value;

        //Act
        ErrorOr<Updated> result = sut.AddQuantity(quantity);

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
    public void AddQuantity_WhenInvalidDataProvided_ThenFailureIsReturned(double quantity)
    {
        //Arrange
        IDateTime DateTimeProviderSubsititute = Substitute.For<IDateTime>();
        TransportUnitId transportUnitId = new(1);
        UserId userId = new(Guid.NewGuid());
        Scan sut = Scan.Create(transportUnitId, userId, DateTimeProviderSubsititute).Value;

        //Act
        ErrorOr<Updated> result = sut.AddQuantity(quantity);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Scans.InvalidScan);
    }
}
