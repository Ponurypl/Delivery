using ErrorOr;
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
    public void Create_UniqueUnitTypeTransportUnit_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string number = "number";
        string? additionalInformation = "additionalInformation";
        string description = "Description";
        string? barcode = "Barcode1234";

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

        obj.MultiUnitDetails.Should().BeNull();
        obj.UniqueUnitDetails.Should().NotBeNull();
        obj.UniqueUnitDetails!.Barcode.Should().Be(barcode);
        obj.UniqueUnitDetails.Id.Should().Be(UniqueUnitDetailsId.Empty);
        obj.UniqueUnitDetails.TransportUnit.Should().Be(obj);

        obj.Recipient.Country.Should().Be(_recipient.Country);
        obj.Recipient.Town.Should().Be(_recipient.Town);
        obj.Recipient.PostCode.Should().Be(_recipient.PostCode);
        obj.Recipient.Street.Should().Be(_recipient.Street);
        obj.Recipient.StreetNumber.Should().Be(_recipient.StreetNumber);
        obj.Recipient.FlatNumber.Should().Be(_recipient.FlatNumber);
        obj.Recipient.CompanyName.Should().Be(_recipient.CompanyName);
        obj.Recipient.LastName.Should().Be(_recipient.LastName);
        obj.Recipient.Name.Should().Be(_recipient.Name);
        obj.Recipient.PhoneNumber.Should().Be(_recipient.PhoneNumber);

    }

    [Fact]
    public void Create_MultiUnitTransportUnit_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string number = "number";
        string? additionalInformation = "additionalInformation";
        string description = "Description";
        double? amount = 234.3D;
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

        obj.UniqueUnitDetails.Should().BeNull();
        obj.MultiUnitDetails.Should().NotBeNull();
        obj.MultiUnitDetails!.Amount.Should().Be(amount);
        obj.MultiUnitDetails.UnitOfMeasureId.Should().Be(unitOfMeasureId);
        obj.MultiUnitDetails.Id.Should().Be(MultiUnitDetailsId.Empty);
        obj.MultiUnitDetails.TransportUnit.Should().Be(obj);

        obj.Recipient.Country.Should().Be(_recipient.Country);
        obj.Recipient.Town.Should().Be(_recipient.Town);
        obj.Recipient.PostCode.Should().Be(_recipient.PostCode);
        obj.Recipient.Street.Should().Be(_recipient.Street);
        obj.Recipient.StreetNumber.Should().Be(_recipient.StreetNumber);
        obj.Recipient.FlatNumber.Should().Be(_recipient.FlatNumber);
        obj.Recipient.CompanyName.Should().Be(_recipient.CompanyName);
        obj.Recipient.LastName.Should().Be(_recipient.LastName);
        obj.Recipient.Name.Should().Be(_recipient.Name);
        obj.Recipient.PhoneNumber.Should().Be(_recipient.PhoneNumber);

    }
}
