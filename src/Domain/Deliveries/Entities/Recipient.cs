namespace Delivery.Domain.Deliveries.Entities;

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
}