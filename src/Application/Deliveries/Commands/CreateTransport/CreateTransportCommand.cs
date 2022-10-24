namespace MultiProject.Delivery.Application.Delivieries.CreateTransport;

public sealed class CreateTransportCommand : ICommand<TransportCreatedDto>
{
    public Guid DelivererId { get; set; }
    public string Number { get; set; } = default!;
    public string? AditionalInformation { get; set; }
    public double? TotalWeight { get; set; }
    public DateTime StartDate { get; set; }
    public Guid ManagerId { get; set; }
    public List<TransportUnit> TransportUnits { get; set; } = new();

    //TODO: Czy to nie powinniśmy zamienić czasem z ovverride TransportUnit-a na NewTransportUnit??
    public sealed class TransportUnit 
    {
        public string Description { get; set; } = default!;
        public string Number { get; set; } = default!;
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
        // jeśli unikatowa paczka to musi zostać podany kod kreskowy
        public string? Barcode { get; set; }
        // jeśli nieunikatowa paczka to trzeba podać jednostkę miary i ilść do dostarczenia
        public int? UnitOfMeasureId { get; set; } = null!;
        public double? Amount { get; set; }
    }
}
