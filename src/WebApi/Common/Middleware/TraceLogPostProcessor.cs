using FluentValidation.Results;
using System.Text.Json;

namespace MultiProject.Delivery.WebApi.Common.Middleware;

internal sealed class TraceLogPostProcessor : IGlobalPostProcessor
{
    public async Task PostProcessAsync(object req, object? res, HttpContext ctx, IReadOnlyCollection<ValidationFailure> failures,
                                       CancellationToken ct)
    {
        ILogger<TraceLogPostProcessor> logger = ctx.Resolve<ILogger<TraceLogPostProcessor>>();


        logger.LogInformation(JsonSerializer.Serialize(req));

    }
}