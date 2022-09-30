using MediatR;

namespace MultiProject.Delivery.Application.Deliveries.Command.CreateNewTransport;

public class CreateNewTransportCommandHandler : IRequestHandler<CreateNewTransportCommand>
{
    public Task<Unit> Handle(CreateNewTransportCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
