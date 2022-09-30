using MediatR;

namespace MultiProject.Delivery.Application.Deliveries.Command.CreateNewTransport;

public sealed record CreateNewTransportCommand : IRequest
{
    public string Number { get; set; }
}
