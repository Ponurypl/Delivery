using MultiProject.Delivery.Domain.Common;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Tests.Unit.Helpers;

namespace MultiProject.Delivery.Domain.Tests.Unit;
public class RecipientTests
{
    [Fact]
    public void Create_WhenValidDataProvided_ThenValidObjectReturned()
    {
        //Arrange
        string companyName = DomainFixture.Recipients.CompanyName;
        string country = DomainFixture.Recipients.Country;
        string flatNumber = DomainFixture.Recipients.FlatNumber;
        string lastName = DomainFixture.Recipients.LastName;
        string name = DomainFixture.Recipients.Name;
        string phoneNumber = DomainFixture.Recipients.PhoneNumber;
        string postCode = DomainFixture.Recipients.PostCode;
        string street = DomainFixture.Recipients.Street;
        string streetNumber = DomainFixture.Recipients.StreetNumber;
        string town = DomainFixture.Recipients.Town;

        //Act
        ErrorOr <Recipient> result = Recipient.Create(companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town);

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
    [ClassData(typeof(RecipientCreateWrongData))]
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
