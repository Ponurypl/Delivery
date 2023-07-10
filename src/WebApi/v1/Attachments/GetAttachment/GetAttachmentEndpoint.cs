using MultiProject.Delivery.Application.Attachments.Queries.GetAttachment;
using MultiProject.Delivery.Application.Common.Failures;
using System.Security.Cryptography;
using System.Text;

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

        var response = _mapper.Map<GetAttachmentResponse>(result.Value);

        if (result.Value.FileExtension is not null)
        {
            using var sha1 = SHA1.Create();
            string hash = Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(result.Value.Id.ToString()))).ToLower();
            response.FileUrl = $"/api/v1/attachments/files/{hash}.{result.Value.FileExtension}";
        }

        await SendOkAsync(response, ct);
    }
}
