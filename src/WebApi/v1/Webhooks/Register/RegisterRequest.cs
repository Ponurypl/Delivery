namespace MultiProject.Delivery.WebApi.v1.Webhooks.Register;

public sealed record RegisterRequest
{
    public string Url { get; init; }
    public string? Event { get; init; }

}