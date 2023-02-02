﻿using MultiProject.Delivery.Application.Attachments.Commands.CreateAttachment;

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
        Group<AttachmentsEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateAttachmentRequest req, CancellationToken ct)
    {
        ErrorOr<AttachmentCreatedDto> result = await _sender.Send(_mapper.Map<CreateAttachmentCommand>(req), ct);

        ValidationFailures.AddErrorsAndThrowIfNeeded(result);
        await SendOkAsync(_mapper.Map<CreateAttachmentResponse>(result.Value), ct);
    }
}