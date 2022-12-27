using MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

namespace MultiProject.Delivery.WebApi.v1.Dictionaries.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureEndpoint : Endpoint<CreateUnitOfMeasureRequest>
{
    private readonly ISender _sender;

    public CreateUnitOfMeasureEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Post("");
        Group<DictionaryEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateUnitOfMeasureRequest req, CancellationToken ct)
    {
        ErrorOr<UnitOfMeasureCreatedDto> response = await _sender.Send(new CreateUnitOfMeasureCommand() { Name = req.Name, Symbol = req.Symbol, Description = req.Description }, ct);

        ValidationFailures.AddErrorsAndThrowIfNeeded(response);

        await SendOkAsync(new CreateUnitOfMeasureResponse() { Id = response.Value.Id }, ct);
    }
}
