using NSwag;
using NSwag.Generation.Processors;
using NSwag.Generation.Processors.Contexts;

namespace MultiProject.Delivery.WebApi.Common.Swagger;

public sealed class AddUnauthorizedResponseOperationProcessor : IOperationProcessor
{
    public bool Process(OperationProcessorContext context)
    {
        if (context.OperationDescription.Operation.Security?.Count > 0)
        {
            if (!context.OperationDescription.Operation.Responses
                        .ContainsKey(StatusCodes.Status401Unauthorized.ToString()))
            {
                context.OperationDescription.Operation.Responses.Add(StatusCodes.Status401Unauthorized.ToString(),
                                                                     new OpenApiResponse()
                                                                     {
                                                                         Description = "Unauthorized"
                                                                     });
            }
        }

        return true;
    }
}