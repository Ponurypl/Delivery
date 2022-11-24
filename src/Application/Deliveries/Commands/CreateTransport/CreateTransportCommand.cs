namespace MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;

//TODO:Dodać Validator
public sealed record CreateTransportCommand : ICommand<TransportCreatedDto>
{
    public required Guid DelivererId { get; init; }
    public required string Number { get; init; }
    public required DateTime StartDate { get; init; }
    public required Guid ManagerId { get; init; }
    public required List<TransportUnitToCreate> TransportUnits { get; init; }
    public string? AdditionalInformation { get; init; }
    public double? TotalWeight { get; init; }
}