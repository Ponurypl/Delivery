using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Data;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Entities;
public class TransportTests
{
    private readonly DomainFixture _fixture = new();

    [Fact]
    public void Create_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange

        List<NewTransportUnit> transportUnitsToCreate = _fixture.Transports.GetMixedTransportUnitsDto();
        UserId delivererId = _fixture.Users.GetId();
        UserId managerId = _fixture.Users.GetId();
        string number = _fixture.Transports.Number;
        string additionalInformation = _fixture.Transports.AdditionalInformation;
        double totalWeight = _fixture.Transports.TotalWeight;
        DateTime startDate = new(2023, 03, 2, 15, 48, 0);

        DateTime creationDate = startDate.AddHours(-1);
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, additionalInformation, totalWeight,
                                                     startDate, managerId, dateTimeProvider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeFalse();

        Transport obj = result.Value;
        obj.Should().NotBeNull();
        obj.DelivererId.Should().Be(delivererId);
        obj.Number.Should().Be(number);
        obj.AdditionalInformation.Should().Be(additionalInformation);
        obj.TotalWeight.Should().Be(totalWeight);
        obj.StartDate.Should().Be(startDate);
        obj.ManagerId.Should().Be(managerId);
        obj.CreationDate.Should().Be(creationDate);
        obj.Status.Should().Be(TransportStatus.New);
        obj.Id.Should().Be(TransportId.Empty);
        obj.TransportUnits.Should().HaveCount(transportUnitsToCreate.Count);

        //TODO: GEHENNA (odroczona)

    }

    [Theory]
    [MemberData(nameof(TransportTestsData.Create_InvalidData), MemberType = typeof(TransportTestsData))]
    public void Create_WhenInvalidDataProvided_ThenValidationFailureIsReturned(
        UserId delivererId, string number, DateTime startDate, UserId managerId,
        DateTime creationDate, List<NewTransportUnit> transportUnitsToCreate)
    {
        //Arrange
        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(creationDate);

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, null, null,
                                                     startDate, managerId, dateTimeProvider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransport);
    }

    [Theory]
    [MemberData(nameof(TransportTestsData.Create_InvalidTransportUnitsData), MemberType = typeof(TransportTestsData))]
    public void Create_WhenInvalidTransportUnitsProvided_ThenValidationFailureIsReturned(List<NewTransportUnit> transportUnitsToCreate)
    {
        //Arrange
        UserId delivererId = _fixture.Users.GetId();
        UserId managerId = _fixture.Users.GetId();
        string number = _fixture.Transports.Number;
        DateTime startDate = new(2024, 1, 1);

        IDateTime dateTimeProvider = Substitute.For<IDateTime>();
        dateTimeProvider.UtcNow.Returns(new DateTime(2023, 1, 1));

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, null, null,
                                                     startDate, managerId, dateTimeProvider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransportUnit);
    }

    [Fact]
    public void Create_DependencyIsNotProvided_ThenUnexpectedFailureIsReturned()
    {
        //Arrange
        UserId delivererId = _fixture.Users.GetId();
        string number = _fixture.Transports.Number;
        DateTime startDate = new(2023, 03, 2, 15, 48, 0);
        UserId managerId = _fixture.Users.GetId();
        string additionalInformation = _fixture.Transports.AdditionalInformation;
        double totalWeight = _fixture.Transports.TotalWeight;
        List<NewTransportUnit> transportUnitsToCreate = _fixture.Transports.GetMixedTransportUnitsDto();

        IDateTime dateTimeProvider = null!;

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, additionalInformation, totalWeight,
                                                     startDate, managerId, dateTimeProvider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }

    [Theory]
    [InlineData(TransportStatus.New)]
    [InlineData(TransportStatus.Processing)]
    public void CheckIfScanAble_WhenScannable_ThenSuccessReturned(TransportStatus status)
    {
        //Arrange
        Transport transport = _fixture.Transports.GetTransport(status);

        //Act
        ErrorOr<Success> result = transport.CheckIfScannable();

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Success);
    }

    [Theory]
    [InlineData(TransportStatus.Deleted)]
    [InlineData(TransportStatus.Finished)]
    public void CheckIfScanAble_WhenNotScannable_ThenFailureReturned(TransportStatus status)
    {
        //Arrange
        Transport transport = _fixture.Transports.GetTransport(status);

        //Act
        ErrorOr<Success> result = transport.CheckIfScannable();

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Conflict);
        result.FirstError.Should().Be(DomainFailures.Deliveries.WrongTransportStatus);
    }
}
