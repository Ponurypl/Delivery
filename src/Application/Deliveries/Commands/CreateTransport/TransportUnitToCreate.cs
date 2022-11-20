namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

public sealed record TransportUnitToCreate
{
    public required string Description { get; init; }
    public required string Number { get; init; }
    public string? AdditionalInformation { get; init; }
    public string? RecipientCompanyName { get; init; }
    public string? RecipientName { get; init; }
    public string? RecipientLastName { get; init; }
    public required string RecipientPhoneNumber { get; init; }
    public string? RecipientFlatNumber { get; init; }
    public required string RecipientStreetNumber { get; init; }
    public string? RecipientStreet { get; init; }
    public required string RecipientTown { get; init; }
    public required string RecipientCountry { get; init; }
    public required string RecipientPostCode { get; init; }
    // jeśli unikatowa paczka to musi zostać podany kod kreskowy
    public string? Barcode { get; init; }
    // jeśli nieunikatowa paczka to trzeba podać jednostkę miary i ilść do dostarczenia
    public int? UnitOfMeasureId { get; init; }
    public double? Amount { get; init; }
}