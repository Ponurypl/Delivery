using Microsoft.AspNetCore.StaticFiles;
using MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;
using MultiProject.Delivery.WebApi.v1.Attachments.GetAttachment;
using System.Security.Cryptography;
using System.Text;

namespace MultiProject.Delivery.WebApi.v1.Attachments.CreateAttachment;

public sealed class CreateAttachmentEndpoint : Endpoint<CreateAttachmentRequest, CreateAttachmentResponse>
{
    private readonly FileExtensionContentTypeProvider _contentTypeProvider = new();
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
        CreateAttachmentCommand command = _mapper.Map<CreateAttachmentCommand>(req);
        string extension = string.Empty;
        
        if (req.File is not null)
        {
            _contentTypeProvider.TryGetContentType(req.File.FileName, out string? contentType);
            if (contentType != req.File.ContentType)
            {
                ThrowError("File content type does not match the file extension");
            }

            extension = req.File.FileName.Split('.').Last();
            command.FileExtension = extension;
        }

        ErrorOr<AttachmentCreatedDto> result = await _sender.Send(command, ct);
        
        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        
        if (req.File is not null)
        {
            using var sha1 = SHA1.Create();
            string hash = Convert.ToHexString(sha1.ComputeHash(Encoding.UTF8.GetBytes(result.Value.Id.ToString()))).ToLower();

            string path = "files";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Join(path, hash[..2]);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            
            path = Path.Join(path, $"{hash[2..]}.{extension}");

            await using FileStream fileStream = new(path, FileMode.CreateNew);
            await using Stream incomingStream = req.File.OpenReadStream();

            await incomingStream.CopyToAsync(fileStream, ct);
        }

        await SendCreatedAtAsync<GetAttachmentEndpoint>(new { AttachmentId = result.Value.Id, req.TransportId },
                                                        _mapper.Map<CreateAttachmentResponse>(result.Value),
                                                        cancellation: ct);
    }
}
