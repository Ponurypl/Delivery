namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransportDetails;

public sealed record RecipientDto
{
    public string? CompanyName { get; init; }
    public string? Name { get; init; }
    public string? LastName { get; init; }
    public string PhoneNumber { get; init; }
    public string? FlatNumber { get; init; }
    public string StreetNumber { get; init; }
    public string? Street { get; init; }
    public string Town { get; init; }
    public string Country { get; init; }
    public string PostCode { get; init; }
}