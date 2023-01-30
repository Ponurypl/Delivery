using MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;
using MultiProject.Delivery.Application.Common.Failures;

namespace MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;

public sealed class GetAttachmentEndpoint : Endpoint<GetAttachmentRequest, GetAttachmentResponse>
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public GetAttachmentEndpoint(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    public override void Configure()
    {
        Get("{TransportId}/{AttachmentId}");
        Group<AttachmentsEndpointGroup>();
        Description(b =>
                        {
                            b.Produces(StatusCodes.Status404NotFound);
                        });
        Version(1);
    }

    public override async Task HandleAsync(GetAttachmentRequest req, CancellationToken ct)
    {
        ErrorOr<AttachmentDto> result = await _sender.Send(new GetAttachmentQuery { Id = req.AttachmentId, TransportId = req.TransportId }, ct);

        if (result.IsError && result.Errors.Contains(Failure.AttachmentNotExists))
        {
            await SendNotFoundAsync(ct);
            return;
        }
        ValidationFailures.AddErrorsAndThrowIfNeeded(result);

        await SendOkAsync(_mapper.Map<GetAttachmentResponse>(result.Value), ct);
    }
}
