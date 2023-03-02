using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit;
public class TransportTests
{
    [Fact]
    public void Create_WhenValidDataProvided_ThenNewObjectReturned()
    {
        //Arrange
        UserId delivererId = DomainFixture.Users.GetRandomId;
        string number = "ABC/234/2023-03/434";
        DateTime startdate = new(2023, 03, 2, 15, 48, 0);
        UserId managerId = DomainFixture.Users.GetRandomId;
        string additionalInformation = "Great tests";
        double totalWeight = 43d;
        List<NewTransportUnit> transportUnitsToCreate = DomainFixture.NewTransportUnits.GetFilledList;

        DateTime creationDate = new(2023, 1, 1, 1, 23, 14);
        IDateTime dateprovider = Substitute.For<IDateTime>();
        dateprovider.UtcNow.Returns(creationDate);

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, additionalInformation, totalWeight, startdate, managerId, dateprovider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeFalse();

        Transport obj = result.Value;
        obj.Should().NotBeNull();
        obj.DelivererId.Should().Be(delivererId);
        obj.Number.Should().Be(number);
        obj.AdditionalInformation.Should().Be(additionalInformation);
        obj.TotalWeight.Should().Be(totalWeight);
        obj.StartDate.Should().Be(startdate);
        obj.ManagerId.Should().Be(managerId);
        obj.CreationDate.Should().Be(creationDate);
        obj.Status.Should().Be(TransportStatus.New);
        obj.Id.Should().Be(TransportId.Empty);
        obj.TransportUnits.Should().NotBeEmpty();
    }

    [Theory]
    [ClassData(typeof(TransportCreateWrongData))]
    public void Create_WhenInvalidDataProvided_ThenValidationFailureIsReturned(Guid guidDelivererId, string number, DateTime startDate, Guid guidManagerId, DateTime creationDate, List<NewTransportUnit> transportUnitsToCreate)
    {
        //Arrange
        UserId delivererId = new(guidDelivererId);
        UserId managerId = new(guidManagerId);

        IDateTime dateprovider = Substitute.For<IDateTime>();
        dateprovider.UtcNow.Returns(creationDate);

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, null, null, startDate, managerId, dateprovider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransport);
    }

    [Fact]
    public void Create_DependencyIsNotProvided_ThenUnexpectedFailureIsReturned()
    {
        //Arrange
        UserId delivererId = DomainFixture.Users.GetRandomId;
        string number = "ABC/234/2023-03/434";
        DateTime startdate = new(2023, 03, 2, 15, 48, 0);
        UserId managerId = DomainFixture.Users.GetRandomId;
        string additionalInformation = "Great tests";
        double totalWeight = 43d;
        List<NewTransportUnit> transportUnitsToCreate = DomainFixture.NewTransportUnits.GetFilledList;

        IDateTime dateprovider = null!;

        //Act
        ErrorOr<Transport> result = Transport.Create(delivererId, number, additionalInformation, totalWeight, startdate, managerId, dateprovider, transportUnitsToCreate);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Unexpected);
        result.FirstError.Should().Be(DomainFailures.Common.MissingRequiredDependency);
    }
}
