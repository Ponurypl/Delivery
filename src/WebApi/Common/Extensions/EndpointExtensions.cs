using FluentValidation.Results;
using MultiProject.Delivery.Application.Common.Failures;

namespace MultiProject.Delivery.WebApi.Common.Extensions;

public static class EndpointExtensions
{
    public static void AddErrors(this List<ValidationFailure> validationFailures, List<Error> errors)
    {
        validationFailures.AddRange(errors.Select(error => new ValidationFailure("GeneralErrors", error.Code)));
    }

    public static void AddErrorsAndThrowIfNeeded<T>(this List<ValidationFailure> validationFailures, ErrorOr<T> result)
    {
        if (!result.IsError) return;

        if (result.Errors.Contains(Failure.UnhandledException))
        {
            //TODO: Do zastąpienia - rozsądnego kurwa
            throw new Exception("Unhandled exception encountered");
        }

        validationFailures.AddErrors(result.Errors);

        throw new ValidationFailureException(validationFailures, $"{nameof(AddErrorsAndThrowIfNeeded)}() called!");
    }
}