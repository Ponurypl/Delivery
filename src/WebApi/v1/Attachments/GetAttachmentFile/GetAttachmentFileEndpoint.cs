using Microsoft.AspNetCore.StaticFiles;

namespace MultiProject.Delivery.WebApi.v1.Attachments.GetAttachmentFile;

public sealed class GetAttachmentFileEndpoint : Endpoint<GetAttachmentFileRequest>
{
    private readonly FileExtensionContentTypeProvider _contentTypeProvider = new();

    public override void Configure()
    {
        Get("/files/{File}"); 
        Group<AttachmentsEndpointGroup>();
        Description(b =>
                    {
                        b.Produces(StatusCodes.Status404NotFound);
                    });
        Version(1);
    }

    public override async Task HandleAsync(GetAttachmentFileRequest req, CancellationToken ct)
    {
        string directory = req.File[..2];
        string fileName = req.File[2..];
        string path = Path.Combine(Directory.GetCurrentDirectory(), "files", directory, fileName);
        
        if (!File.Exists(path))
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _contentTypeProvider.TryGetContentType(path, out string? contentType);

        await SendFileAsync(new FileInfo(path), cancellation: ct, contentType: contentType ?? "application/octet-stream");
    }
}