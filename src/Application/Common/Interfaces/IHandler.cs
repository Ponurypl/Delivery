using MediatR;

namespace MultiProject.Delivery.Application.Common.Interfaces;

internal interface IHandler<TCommand> : IRequestHandler<TCommand>
    where TCommand : ICommand
{
}


internal interface IHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{

}


