using Bogus;
using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Data;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.Entities;
public class TransportUnitTests
{
    private readonly DomainFixture _domainFixture = new();
    private readonly Transport _transport;
    private readonly Recipient _recipient;

    public TransportUnitTests()
    {
        _transport = _domainFixture.Transports.GetEmptyTransport();
        _recipient = _domainFixture.Recipients.GetRecipient();
    }

    [Fact]
    public void Create_UniqueUnitTransportUnit_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string number = _domainFixture.TransportUnits.Number;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        string description = _domainFixture.TransportUnits.Description;
        string barcode = _domainFixture.TransportUnits.Barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient,
                                                             barcode, null, null, _transport);

        //Arrange
        result.IsError.Should().BeFalse();

        TransportUnit obj = result.Value;
        obj.Should().NotBeNull();
        obj.Number.Should().Be(number);
        obj.AdditionalInformation.Should().Be(additionalInformation);
        obj.Description.Should().Be(description);
        obj.Id.Should().Be(TransportUnitId.Empty);
        obj.Status.Should().Be(TransportUnitStatus.New);
        obj.Transport.Should().Be(_transport);
        obj.Recipient.Should().Be(_recipient);

        obj.MultiUnitDetails.Should().BeNull();
        obj.UniqueUnitDetails.Should().NotBeNull();
        obj.UniqueUnitDetails!.Barcode.Should().Be(barcode);
        obj.UniqueUnitDetails.Id.Should().Be(UniqueUnitDetailsId.Empty);
        obj.UniqueUnitDetails.TransportUnit.Should().Be(obj);
    }

    [Fact]
    public void Create_MultiUnitTransportUnit_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string number = _domainFixture.TransportUnits.Number;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        string description = _domainFixture.TransportUnits.Description;
        double amount = _domainFixture.TransportUnits.Amount;
        UnitOfMeasureId unitOfMeasureId = _domainFixture.UnitOfMeasures.GetId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient,
                                                             null, amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeFalse();

        TransportUnit obj = result.Value;
        obj.Should().NotBeNull();
        obj.Number.Should().Be(number);
        obj.AdditionalInformation.Should().Be(additionalInformation);
        obj.Description.Should().Be(description);
        obj.Id.Should().Be(TransportUnitId.Empty);
        obj.Status.Should().Be(TransportUnitStatus.New);
        obj.Transport.Should().Be(_transport);
        obj.Recipient.Should().Be(_recipient);

        obj.UniqueUnitDetails.Should().BeNull();
        obj.MultiUnitDetails.Should().NotBeNull();
        obj.MultiUnitDetails!.Amount.Should().Be(amount);
        obj.MultiUnitDetails.UnitOfMeasureId.Should().Be(unitOfMeasureId);
        obj.MultiUnitDetails.Id.Should().Be(MultiUnitDetailsId.Empty);
        obj.MultiUnitDetails.TransportUnit.Should().Be(obj);
    }

    [Theory]
    [MemberData(nameof(TransportUnitTestData.Create_InvalidData), MemberType = typeof(TransportUnitTestData))]
    public void Create_UniqueUnitTransportUnit_WhenInvalidDataProvided_ThenFailureReturned(string number,  string description)
    {
        //Arrange
        string barcode = _domainFixture.TransportUnits.Barcode;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient,
                                                             barcode, null, null, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransportUnit);
    }

    [Theory]
    [MemberData(nameof(TransportUnitTestData.Create_InvalidData), MemberType = typeof(TransportUnitTestData))]
    public void Create_MultiUnitTransportUnit_WhenInvalidDataProvided_ThenFailureReturned(string number, string description)
    {
        //Arrange
        double amount = _domainFixture.TransportUnits.Amount;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        UnitOfMeasureId unitOfMeasureId = _domainFixture.UnitOfMeasures.GetId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient,
                                                             null, amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransportUnit);
    }

    [Theory]
    [MemberData(nameof(TransportUnitTestData.Create_Details_InvalidData), MemberType = typeof(TransportUnitTestData))]
    public void Create_WhenInvalidTransportUnitDetailsDataProvided_ThenFailureReturned(
        string? barcode, double? amount, UnitOfMeasureId? unitOfMeasureId)
    {
        //Arrange

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(_domainFixture.TransportUnits.Number,
                                                             _domainFixture.TransportUnits.AdditionalInformation,
                                                             _domainFixture.TransportUnits.Description, _recipient,
                                                             barcode, amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransportUnitDetails);
    }

    [Fact]
    public void Create_UniqueUnitTransportUnit_WhenParentObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = _domainFixture.TransportUnits.Number;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        string description = _domainFixture.TransportUnits.Description;
        string barcode = _domainFixture.TransportUnits.Barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient,
                                                             barcode, null, null, null!);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingParentObject);
    }

    [Fact]
    public void Create_MultiUnitTransportUnit_WhenParentObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = _domainFixture.TransportUnits.Number;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        string description = _domainFixture.TransportUnits.Description;
        double amount = _domainFixture.TransportUnits.Amount;
        UnitOfMeasureId unitOfMeasureId = _domainFixture.UnitOfMeasures.GetId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient,
                                                             null, amount, unitOfMeasureId, null!);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingParentObject);
    }

    [Fact]
    public void Create_UniqueUnitTransportUnit_WhenChildObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = _domainFixture.TransportUnits.Number;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        string description = _domainFixture.TransportUnits.Description;
        string barcode = _domainFixture.TransportUnits.Barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, null!,
                                                             barcode, null, null, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingChildObject);
    }

    [Fact]
    public void Create_MultiUnitTransportUnit_WhenChildObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = _domainFixture.TransportUnits.Number;
        string additionalInformation = _domainFixture.TransportUnits.AdditionalInformation;
        string description = _domainFixture.TransportUnits.Description;
        double amount = _domainFixture.TransportUnits.Amount;
        UnitOfMeasureId unitOfMeasureId = _domainFixture.UnitOfMeasures.GetId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, null!,
                                                             null, amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingChildObject);
    }

    [Theory]
    [InlineData(TransportUnitStatus.New)]
    [InlineData(TransportUnitStatus.PartiallyDelivered)]
    public void CheckIfScanAble_WhenScannable_ThenSuccessReturned(TransportUnitStatus status)
    {
        //Arrange
        TransportUnit transportUnit = _domainFixture.TransportUnits.GetTransportUnit(status);

        //Act
        ErrorOr<Success> result = transportUnit.CheckIfScannable();

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Success);
    }

    [Theory]
    [InlineData(TransportUnitStatus.Deleted)]
    [InlineData(TransportUnitStatus.Delivered)]
    [InlineData((TransportUnitStatus)99999)]
    public void CheckIfScanAble_WhenNotScannable_ThenFailureReturned(TransportUnitStatus status)
    {
        //Arrange
        TransportUnit transportUnit = _domainFixture.TransportUnits.GetTransportUnit(status);

        //Act
        ErrorOr<Success> result = transportUnit.CheckIfScannable();

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Conflict);
        result.FirstError.Should().Be(DomainFailures.Deliveries.TransportUnitStatusError);
    }

    [Fact]
    public void UpdateStatus_WhenValidStatusProvided_ThenStatusUpdated()
    {
        //Arrange
        TransportUnit sut = _domainFixture.TransportUnits.GetTransportUnit();
        const TransportUnitStatus statusToChange = TransportUnitStatus.Delivered;

        //Act
        ErrorOr<Updated> result = sut.UpdateStatus(statusToChange);

        //Assert
        result.IsError.Should().BeFalse();
        result.Value.Should().Be(Result.Updated);
        sut.Status.Should().Be(statusToChange);
    }

    [Fact]
    public void UpdateStatus_WhenInValidStatusProvided_ThenStatusUpdated()
    {
        //Arrange
        TransportUnit sut = _domainFixture.TransportUnits.GetTransportUnit();
        const TransportUnitStatus statusToChange = (TransportUnitStatus) 99999;

        //Act
        ErrorOr<Updated> result = sut.UpdateStatus(statusToChange);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransportUnit);
        sut.Status.Should().NotBe(statusToChange);
    }
}
