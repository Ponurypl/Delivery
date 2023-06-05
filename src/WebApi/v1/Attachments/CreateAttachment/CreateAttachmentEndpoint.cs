using MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;
using MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;
using System.Text;

namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

public sealed class CreateAttachmentEndpoint : Endpoint<CreateAttachmentRequest, CreateAttachmentResponse>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;
    public CreateAttachmentEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Post("");
        AllowFileUploads();
        Group<AttachmentsEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateAttachmentRequest req, CancellationToken ct)
    {
        var command = _mapper.Map<CreateAttachmentCommand>(req);
        
        if (req.File is not null)
        {
            using var sr = new StreamReader(req.File.OpenReadStream());
            command.Payload = Encoding.UTF8.GetBytes(await sr.ReadToEndAsync(ct));
            command.FileExtension = MapToFileExtensionFormat(req.File.ContentType);
            command.FileName = req.File.FileName;
        }

        ErrorOr<AttachmentCreatedDto> result = await _sender.Send(command, ct);
        
        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendCreatedAtAsync<GetAttachmentEndpoint>(
            new { AttachmentId = result.Value.Id, TransportId = req.TransportId },
            _mapper.Map<CreateAttachmentResponse>(result.Value),
            generateAbsoluteUrl: true,
            cancellation: ct);
    }

    private string MapToFileExtensionFormat(string contentType)
    {
        //TODO: Zmiana na słownik wspólny z walidatorem
        return contentType switch
               {
                   "image/jpeg" => "jpg",
                   "image/png" => "png",
                   "application/pdf" => "pdf",
                   "video/mp4" => "mp4",
                   "text/plain" => "txt",
                   _ => throw new ArgumentException("Unsupported file type", nameof(contentType))
               };
    }
}
