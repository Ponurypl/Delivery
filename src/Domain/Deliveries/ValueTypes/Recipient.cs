using MultiProject.Delivery.Domain.Common.Abstractions;

namespace MultiProject.Delivery.Domain.Deliveries.ValueTypes;

public sealed class Recipient : ValueObject
{
    public string? CompanyName { get; private set; }
    public string? Name { get; private set; }
    public string? LastName { get; private set; }
    public string PhoneNumber { get; private set; }
    public string? FlatNumber { get; private set; }
    public string StreetNumber { get; private set; }
    public string? Street { get; private set; }
    public string Town { get; private set; }
    public string Country { get; private set; }
    public string PostCode { get; private set; }

    
#pragma warning disable CS8618, IDE0051
    private Recipient() { }
#pragma warning restore 

    private Recipient(string? companyName, string country, string? flatNumber, string? lastName,
                      string? name, string phoneNumber, string postCode, string? street,
                      string streetNumber, string town)
    {
        CompanyName = companyName;
        Country = country;
        FlatNumber = flatNumber;
        LastName = lastName;
        Name = name;
        PhoneNumber = phoneNumber;
        PostCode = postCode;
        Street = street;
        StreetNumber = streetNumber;
        Town = town;
    }

    public static ErrorOr<Recipient> Create(string? companyName, string country, string? flatNumber, string? lastName,
                                            string? name, string phoneNumber, string postCode, string? street,
                                            string streetNumber, string town)
    {
        if (string.IsNullOrWhiteSpace(town) || string.IsNullOrWhiteSpace(postCode) ||
            string.IsNullOrWhiteSpace(streetNumber) || string.IsNullOrWhiteSpace(country) ||
            string.IsNullOrWhiteSpace(phoneNumber))
        {
            return DomainFailures.Deliveries.InvalidRecipient;
        }

        if (string.IsNullOrWhiteSpace(companyName) && string.IsNullOrWhiteSpace(name) &&
            string.IsNullOrWhiteSpace(lastName))
        {
            return DomainFailures.Deliveries.InvalidRecipient;
        }

        if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName) ||
            !string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName))
        {
            return DomainFailures.Deliveries.InvalidRecipient;
        }

        return new Recipient(companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street,
                             streetNumber, town);
    }

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return CompanyName;
        yield return Country;
        yield return FlatNumber;
        yield return LastName;
        yield return Name;
        yield return PhoneNumber;
        yield return PostCode;
        yield return Street;
        yield return StreetNumber;
        yield return Town;
    }
}