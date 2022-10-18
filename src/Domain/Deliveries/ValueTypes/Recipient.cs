using MultiProject.Delivery.Domain.Deliveries.Validators;

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
        var recipient = new Recipient(companyName, country, flatNumber, lastName, name, phoneNumber, postCode, street, streetNumber, town);
        var validator = new RecipientValidator();

        var vResults = validator.Validate(recipient);
        if (!vResults.IsValid)
        {
            throw new ValidationException(vResults.Errors);
        }

        return recipient;
    }
}
