﻿namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

/// <summary>
/// At least one TransportUnitRequest needs to be provded to create Deliviery
/// </summary>
public sealed record CreateTransportRequest
{
    public Guid DelivererId { get; init; }
    public string Number { get; init; } = default!;
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
    public List<TransportUnitRequest> TransportUnits { get; init; } = new();
    public string? AdditionalInformation { get; init; }
    public double? TotalWeight { get; init; }
}
