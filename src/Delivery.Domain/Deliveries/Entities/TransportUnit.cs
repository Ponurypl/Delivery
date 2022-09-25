namespace Delivery.Domain.Deliveries.Entities;

public sealed class TransportUnit
{
    public int Id { get; set; }
    public Transport Transport { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Status { get; set; }
    public int? RecipientID { get; set; }
    public string? AditionalInformation { get; set; }
    public string? RecipientCompanyName { get; set; }
    public string? RecipientName { get; set; }
    public string? RecipientLastName { get; set; }
    public string RecipientPhoneNumber { get; set; } = default!;
    public string? RecipientFlatNumber { get; set; }
    public string RecipientStreetNumber { get; set; } = default!;
    public string? RecipientStreet { get; set; }
    public string RecipientTown { get; set; } = default!;
    public string RecipientCountry { get; set; } = default!;
    public string RecipientPostCode { get; set; } = default!;
    public UnitDetails UnitDetails { get; set; } = null!;
}
