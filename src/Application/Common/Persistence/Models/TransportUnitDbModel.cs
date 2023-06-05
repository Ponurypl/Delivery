namespace MultiProject.Delivery.Application.Common.Persistence.Models;

public sealed record TransportUnitDbModel
{
    public int Id { get; init; }
    public string Number { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Status { get; init; } = default!;
    public string? AdditionalInformation { get; init; }
    public RecipientDbModel Recipient { get; set; } = null!;
    public string? Barcode { get; init; }
    public int? UnitOfMeasureId { get; init; }
    public double? Amount { get; init; }
}