namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public sealed record ResponseRecipientDto
{
    public string? CompanyName { get; init; }
    public string? Name { get; init; }
    public string? LastName { get; init; }
    public string PhoneNumber { get; init; } = default!;
    public string? FlatNumber { get; init; }
    public string StreetNumber { get; init; } = default!;
    public string? Street { get; init; }
    public string Town { get; init; } = default!;
    public string Country { get; init; } = default!;
    public string PostCode { get; init; } = default!;
}