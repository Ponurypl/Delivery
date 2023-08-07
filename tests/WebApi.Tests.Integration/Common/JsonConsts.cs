using System.Text.Json;

namespace MultiProject.Delivery.WebApi.Tests.Integration.Common;

public class JsonConsts
{
    public static JsonSerializerOptions Defaults { get; } = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
}