using MediatR;

namespace MultiProject.Delivery.Application.Common.Messaging;

internal interface IQueryHandler<in TQuery> : IRequestHandler<TQuery, ErrorOr<Success>>
    where TQuery : IQuery
{
}


internal interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, ErrorOr<TResponse>>
    where TQuery : IQuery<TResponse>
{

}