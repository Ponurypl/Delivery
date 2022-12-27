using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;

namespace MultiProject.Delivery.WebApi.v1.Dictionaries.GetUnitOfMeasure;

public sealed class GetUnitOfMeasureEndpoint : Endpoint<GetUnitOfMeasureRequest, GetUnitOfMeasureResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetUnitOfMeasureEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("{UnitOfMeasureId}");
        Group<DictionaryEndpointGroup>();
        Description(d =>
                    {
                        d.Produces(StatusCodes.Status404NotFound);
                    });
        Version(1);
    }

    public override async Task HandleAsync(GetUnitOfMeasureRequest req, CancellationToken ct)
    {
        ErrorOr<GetUnitOfMeasureDto> response = await _sender.Send(new GetUnitOfMeasureQuery() { Id = req.UnitOfMeasureId }, ct);

        if (response.IsError && response.Errors.Contains(Failure.UnitOfMeasureNotExists))
        {
            await SendNotFoundAsync(ct);
        }

        ValidationFailures.AddErrorsAndThrowIfNeeded(response);

        await SendOkAsync(_mapper.Map<GetUnitOfMeasureResponse>(response.Value), ct);
    }
}
