using MediatR;
using Microsoft.Extensions.Logging;
using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Logging;

namespace MultiProject.Delivery.Application.Common.Behaviors;

internal sealed class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> _logger;

    public UnhandledExceptionBehavior(ILogger<UnhandledExceptionBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception e)
        {
            LogDefinitions.UnhandledException(_logger, e);
            return (dynamic)Failure.UnhandledException;
        }
    }
}