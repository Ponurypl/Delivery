using MultiProject.Delivery.Domain.Deliveries.Exceptions;

namespace MultiProject.Delivery.Domain.Deliveries.ValueTypes;

public sealed class Recipient
{
    public string? CompanyName { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string? FlatNumber { get; set; }
    public string StreetNumber { get; set; } = default!;
    public string? Street { get; set; }
    public string Town { get; set; } = default!;
    public string Country { get; set; } = default!;
    public string PostCode { get; set; } = default!;

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

    public static Recipient Create(string? companyName, string country, string? flatNumber, string? lastName,
                     string? name, string phoneNumber, string postCode, string? street,
                     string streetNumber, string town)
    {
        if (string.IsNullOrWhiteSpace(companyName) && string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName)) throw new TransportUnitRecipientNoRecipientName();
        if (!string.IsNullOrWhiteSpace(name) && string.IsNullOrWhiteSpace(lastName)) throw new TransportUnitRecipientNameAndLastNameNotGivenTogether(nameof(lastName));
        if (string.IsNullOrWhiteSpace(name) && !string.IsNullOrWhiteSpace(lastName)) throw new TransportUnitRecipientNameAndLastNameNotGivenTogether(nameof(name));

        if (string.IsNullOrWhiteSpace(phoneNumber)) throw new ArgumentException($"'{nameof(phoneNumber)}' cannot be null or whitespace.", nameof(phoneNumber));
        if (string.IsNullOrWhiteSpace(postCode)) throw new ArgumentException($"'{nameof(postCode)}' cannot be null or whitespace.", nameof(postCode));
        if (string.IsNullOrWhiteSpace(streetNumber)) throw new ArgumentException($"'{nameof(streetNumber)}' cannot be null or whitespace.", nameof(streetNumber));
        if (string.IsNullOrWhiteSpace(country)) throw new ArgumentException($"'{nameof(country)}' cannot be null or whitespace.", nameof(country));
        if (string.IsNullOrWhiteSpace(town)) throw new ArgumentException($"'{nameof(town)}' cannot be null or whitespace.", nameof(town));


        return new Recipient(companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town);
    }
}