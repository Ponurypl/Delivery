﻿using MediatR;
using Microsoft.Extensions.Logging;
using MultiProject.Delivery.Application.Common.Logging;
using System.Text.Json;

namespace MultiProject.Delivery.Application.Common.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse: IErrorOr
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
                              ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
                                        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                           .Where(r => r.Errors.Any())
                           .SelectMany(r => r.Errors)
                           .ToList();

            if (failures.Any())
            {
                string failuresjson = JsonSerializer.Serialize(failures);
                string requestjson = JsonSerializer.Serialize(request);
                LogDefinitions.ValidationFailures(_logger, typeof(TRequest).FullName!, requestjson, failuresjson) ;
                return (dynamic)Failures.Failure.InvalidMessage;
            }
        }

        return await next();
    }
}