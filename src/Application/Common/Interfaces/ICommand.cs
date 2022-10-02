using MediatR;

namespace MultiProject.Delivery.Application.Common.Interfaces;

internal interface ICommand : IRequest
{
}

internal interface ICommand<TResponse> : IRequest<TResponse>
{
}