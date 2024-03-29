﻿using MultiProject.Delivery.Application.Deliveries.Commands.CreateTransport;
using MultiProject.Delivery.WebApi.v1.Deliveries.GetTransportDetails;

namespace MultiProject.Delivery.WebApi.v1.Deliveries.CreateTransport;

public sealed class CreateTransportEndpoint : Endpoint<CreateTransportRequest, CreateTransportResponse>
{
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public CreateTransportEndpoint(IMapper mapper, ISender sender)
    {
        _mapper = mapper;
        _sender = sender;
    }

    public override void Configure()
    {
        Post("");
        Group<DeliveriesEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(CreateTransportRequest req, CancellationToken ct)
    {
        ErrorOr<TransportCreatedDto> response = await _sender.Send(_mapper.Map<CreateTransportCommand>(req),ct);
        ValidationFailures.AddErrorsAndThrowIfNeeded(response);
        await SendCreatedAtAsync<GetTransportDetailsEndpoint>(
            new { TransportId = response.Value.Id },
            _mapper.Map<CreateTransportResponse>(response.Value), 
            generateAbsoluteUrl: true,
            cancellation: ct);
    }
}
