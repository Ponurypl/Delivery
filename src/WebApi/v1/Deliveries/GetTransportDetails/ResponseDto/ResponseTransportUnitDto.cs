namespace MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

public class ResponseTransportUnitDto
{
    public int Id { get; init; }
    public string Number { get; init; } = default!;
    public string Description { get; init; } = default!;
    public string Status { get; init; } = default!;
    public string? AdditionalInformation { get; init; }
    public ResponseRecipientDto Recipient { get; init; } = null!;
    public string? Barcode { get; init; }
    public int? UnitOfMeasureId { get; init; }
    public double? Amount { get; init; }
    public List<int> Scans { get; init; } = new();
    public List<int> Attachments { get; init; } = new();
}