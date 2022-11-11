using MediatR;

namespace MultiProject.Delivery.Application.Common.Messaging;

internal interface ICommand : IRequest<ErrorOr<Success>>
{
}

internal interface ICommand<TResponse> : IRequest<ErrorOr<TResponse>>
{
}