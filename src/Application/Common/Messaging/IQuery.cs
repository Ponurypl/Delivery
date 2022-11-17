using MediatR;

namespace MultiProject.Delivery.Application.Common.Messaging;

internal interface IQuery : IRequest<ErrorOr<Success>>
{
}

internal interface IQuery<TResponse> : IRequest<ErrorOr<TResponse>>
{
}