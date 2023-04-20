using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Data;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit.ValueTypes;
public class RecipientTests
{
    private readonly DomainFixture _domainFixture = new();
    [Fact]
    public void Create_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string companyName = _domainFixture.Recipients.CompanyName;
        string country = _domainFixture.Recipients.Country;
        string flatNumber = _domainFixture.Recipients.FlatNumber;
        string lastName = _domainFixture.Recipients.LastName;
        string name = _domainFixture.Recipients.Name;
        string phoneNumber = _domainFixture.Recipients.PhoneNumber;
        string postCode = _domainFixture.Recipients.PostCode;
        string street = _domainFixture.Recipients.Street;
        string streetNumber = _domainFixture.Recipients.StreetNumber;
        string town = _domainFixture.Recipients.Town;

        //Act
        ErrorOr<Recipient> result = Recipient.Create(companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town);

        //Assert
        result.IsError.Should().BeFalse();

        Recipient obj = result.Value;
        obj.Should().NotBeNull();
        obj.CompanyName.Should().Be(companyName);
        obj.Country.Should().Be(country);
        obj.FlatNumber.Should().Be(flatNumber);
        obj.LastName.Should().Be(lastName);
        obj.Name.Should().Be(name);
        obj.PhoneNumber.Should().Be(phoneNumber);
        obj.PostCode.Should().Be(postCode);
        obj.Street.Should().Be(street);
        obj.StreetNumber.Should().Be(streetNumber);
        obj.Town.Should().Be(town);
    }

    [Theory]
    [MemberData(nameof(RecipientCreateTestData.Create_InvalidData), MemberType = typeof(RecipientCreateTestData))]
    public void Create_WhenInvalidDataProvided_ThenFailureReturned(string? companyName, string country, string? flatNumber, string? lastName,
                                            string? name, string phoneNumber, string postCode, string? street,
                                            string streetNumber, string town)
    {
        //Arrange

        //Act
        ErrorOr<Recipient> result = Recipient.Create(companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town);

        //Assert
        result.IsError.Should().BeTrue();
        result.FirstError.Type.Should().Be(ErrorType.Validation);
        result.FirstError.Should().Be(DomainFailures.Deliveries.InvalidRecipient);
        result.Value.Should().BeNull();
    }

}
