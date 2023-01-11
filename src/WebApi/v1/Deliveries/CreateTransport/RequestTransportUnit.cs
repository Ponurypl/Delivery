namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

/// <summary>
/// if package is of "Unique" type only barcode needs to be given eg. courier delivery
/// if package is of "Multi" type only UnitOfMeasure and Amount needs to be given eg. 2t of sand, 2kg of potatoes
/// providing all 3 fields will result in ValidationError, as type of package could not be determined.
/// </summary>
public sealed record RequestTransportUnit
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
    public required string RecipientCountry { get; init; }
    public string RecipientPostCode { get; init; } = default!;
    // jeśli unikatowa paczka to musi zostać podany kod kreskowy
    public string? Barcode { get; init; }
    // jeśli nieunikatowa paczka to trzeba podać jednostkę miary i ilść do dostarczenia
    public int? UnitOfMeasureId { get; init; }
    public double? Amount { get; init; }
}