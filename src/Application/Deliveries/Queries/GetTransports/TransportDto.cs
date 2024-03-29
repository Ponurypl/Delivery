﻿namespace MultiProject.Delivery.Application.Deliveries.Queries.GetTransports;

public sealed record TransportDto
{
    public int Id { get; init; }
    public Guid DelivererId { get; init; }
    public string Status { get; init; } = default!;
    public string Number { get; init; } = default!;
    public DateTime CreationDate { get; init; }
    public DateTime StartDate { get; init; }
    public Guid ManagerId { get; init; }
}