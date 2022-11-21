using FluentValidation;
using MediatR;

namespace MultiProject.Delivery.Application.Common.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, ErrorOr<TResponse>>
    where TRequest : IRequest<ErrorOr<TResponse>>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<ErrorOr<TResponse>> Handle(TRequest request, RequestHandlerDelegate<ErrorOr<TResponse>> next,
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
                return Failures.Failure.InvalidMessage;
        }

        return await next();
    }
}