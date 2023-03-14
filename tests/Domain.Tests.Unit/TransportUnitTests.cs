using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit;
public class TransportUnitTests
{
    private readonly Transport _transport;
    private readonly Recipient _recipient;

    public TransportUnitTests()
    {
        _transport = DomainFixture.Transports.GetEmptyTransport();
        _recipient = DomainFixture.Recipients.GetRecipient();
    }

    [Fact]
    public void Create_UniqueUnitTransportUnit_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string number = DomainFixture.TransportUnits.number;
        string additionalInformation = DomainFixture.TransportUnits.additionalInformation;
        string description = DomainFixture.TransportUnits.description;
        string barcode = DomainFixture.TransportUnits.barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient, barcode, null, null, _transport);

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
        string number = DomainFixture.TransportUnits.number;
        string additionalInformation = DomainFixture.TransportUnits.additionalInformation;
        string description = DomainFixture.TransportUnits.description;
        double amount = DomainFixture.TransportUnits.amount;
        UnitOfMeasureId unitOfMeasureId = DomainFixture.UnitOfMeasures.GetRandomId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient, null, amount, unitOfMeasureId, _transport);

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
    [ClassData(typeof(TransportUnitCreateWrongData))]
    public void Create_UniqueUnitTransportUnit_WhenInvalidDataProvided_ThenFailureReturned(string number, string additionalInformation, string description)
    {
        //Arrange
        string barcode = DomainFixture.TransportUnits.barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient, barcode, null, null, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }
    [Theory]
    [ClassData(typeof(TransportUnitCreateWrongData))]
    public void Create_MultiUnitTransportUnit_WhenInvalidDataProvided_ThenFailureReturned(string number, string additionalInformation, string description)
    {
        //Arrange
        double amount = DomainFixture.TransportUnits.amount;
        UnitOfMeasureId unitOfMeasureId = DomainFixture.UnitOfMeasures.GetRandomId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient, null, amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }

    [Theory]
    [ClassData(typeof(TransportUnitCreateWrongTransportUnitDetailsData))]
    public void Create_WhenInvalidTransportUnitDetailsDataProvided_ThenFailureReturned(string? barcode, double? amount, int? rawUnitOfMeasureId)
    {
        //Arrange
        UnitOfMeasureId? unitOfMeasureId = rawUnitOfMeasureId is null ? null : new UnitOfMeasureId((int)rawUnitOfMeasureId);

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(DomainFixture.TransportUnits.number,
                                                             DomainFixture.TransportUnits.additionalInformation,
                                                             DomainFixture.TransportUnits.description, _recipient, barcode,
                                                             amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidTransportUnitDetails);
    }

    [Fact]
    public void Create_UniqueUnitTransportUnit_WhenParentObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = DomainFixture.TransportUnits.number;
        string additionalInformation = DomainFixture.TransportUnits.additionalInformation;
        string description = DomainFixture.TransportUnits.description;
        string barcode = DomainFixture.TransportUnits.barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient, barcode, null, null, null!);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
    }

    [Fact]
    public void Create_MultiUnitTransportUnit_WhenParentObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = DomainFixture.TransportUnits.number;
        string additionalInformation = DomainFixture.TransportUnits.additionalInformation;
        string description = DomainFixture.TransportUnits.description;
        double amount = DomainFixture.TransportUnits.amount;
        UnitOfMeasureId unitOfMeasureId = DomainFixture.UnitOfMeasures.GetRandomId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, _recipient, null, amount, unitOfMeasureId, null!);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingParentObject);
    }

    [Fact]
    public void Create_UniqueUnitTransportUnit_WhenChildObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = DomainFixture.TransportUnits.number;
        string additionalInformation = DomainFixture.TransportUnits.additionalInformation;
        string description = DomainFixture.TransportUnits.description;
        string barcode = DomainFixture.TransportUnits.barcode;

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, null!, barcode, null, null, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingChildObject);
    }

    [Fact]
    public void Create_MultiUnitTransportUnit_WhenChildObjectNotProvided_ThenFailureReturned()
    {
        //Arrange
        string number = DomainFixture.TransportUnits.number;
        string additionalInformation = DomainFixture.TransportUnits.additionalInformation;
        string description = DomainFixture.TransportUnits.description;
        double amount = DomainFixture.TransportUnits.amount;
        UnitOfMeasureId unitOfMeasureId = DomainFixture.UnitOfMeasures.GetRandomId();

        //Act
        ErrorOr<TransportUnit> result = TransportUnit.Create(number, additionalInformation, description, null!, null, amount, unitOfMeasureId, _transport);

        //Arrange
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Common.MissingChildObject);
    }


}
