using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace MultiProject.Delivery.Application.Webhooks.Events.ScanCreated;
internal class ScanCreatedEventHandler : IEventHandler<ScanCreatedEvent>
{
    private readonly HttpClient _client;

    public ScanCreatedEventHandler(HttpClient client)
    {
        _client = client;
    }

    public async Task Handle(ScanCreatedEvent notification, CancellationToken cancellationToken)
    {
        JsonContent message = JsonContent.Create(notification, MediaTypeHeaderValue.Parse("application/json")); //todo: przenieść do infrastruktury samą wysyłkę komunikatu
        Uri requestUri = new("https://webhook.site/6bdca34c-aae8-435d-beaf-0d6b882ddfcd");
        HttpResponseMessage response = await _client.PostAsync(requestUri, message, cancellationToken);
        string responseString = await response.Content.ReadAsStringAsync(cancellationToken);
    }
}
