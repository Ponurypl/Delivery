using System.Text.Json;

namespace MultiProject.Delivery.WebApi.Tests.Integration.Common;

public static class HttpContentExtensions
{
    public static async Task<T> ReadAsAsync<T>(this HttpContent content)
    {
        var json = await content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, JsonConsts.Defaults)!;
    }
    
}