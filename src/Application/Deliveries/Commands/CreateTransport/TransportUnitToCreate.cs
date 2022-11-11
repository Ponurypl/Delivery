namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

public sealed record TransportUnitToCreate
{
    public string Description { get; init; } = default!;
    public string Number { get; init; } = default!;
    public string? AdditionalInformation { get; init; }
    public string? RecipientCompanyName { get; init; }
    public string? RecipientName { get; init; }
    public string? RecipientLastName { get; init; }
    public string RecipientPhoneNumber { get; init; } = default!;
    public string? RecipientFlatNumber { get; init; }
    public string RecipientStreetNumber { get; init; } = default!;
    public string? RecipientStreet { get; init; }
    public string RecipientTown { get; init; } = default!;
    public string RecipientCountry { get; init; } = default!;
    public string RecipientPostCode { get; init; } = default!;
    // jeśli unikatowa paczka to musi zostać podany kod kreskowy
    public string? Barcode { get; init; }
    // jeśli nieunikatowa paczka to trzeba podać jednostkę miary i ilść do dostarczenia
    public int? UnitOfMeasureId { get; init; } = null!;
    public double? Amount { get; init; }
}