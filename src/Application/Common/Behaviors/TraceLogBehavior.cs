﻿using MediatR;
using Microsoft.Extensions.Logging;
using MultiProject.Delivery.Application.Common.Logging;
using System.Text.Json;

namespace MultiProject.Delivery.Application.Common.Behaviors;

internal sealed class TraceLogBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<TraceLogBehavior<TRequest, TResponse>> _logger;

    public TraceLogBehavior(ILogger<TraceLogBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_logger.IsEnabled(LogLevel.Trace))
        {
            return await next();
        }

        //TODO: Zabezpieczyć przed wylotem ?
        string req = JsonSerializer.Serialize(request);
        TResponse response = await next();
        string resp = JsonSerializer.Serialize(response);
        
        LogDefinitions.RequestProcessingTrace(_logger, typeof(TRequest).FullName!, req, resp);

        return response;
    }
}