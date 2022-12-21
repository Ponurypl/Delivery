using ErrorOr;
using MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUserLocation;

public sealed class UpdateUserLocationEndpoint : Endpoint<UpdateUserLocationRequest>
{
    private readonly ISender _sender;

    public UpdateUserLocationEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("{UserId}/location");
        Group<UsersEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(UpdateUserLocationRequest req, CancellationToken ct)
    {
        ErrorOr<Success> response = await _sender.Send(new UpdateUserLocationCommand() 
                                                      { 
                                                        UserId = req.UserId,
                                                        Latitude = req.Latitude,
                                                        Longitude = req.Longitude,
                                                        Accuracy = req.Accuracy,
                                                        Heading = req.Heading,
                                                        Speed = req.Speed
                                                      }, ct);
        
        ValidationFailures.AddErrorsAndThrowIfNeeded(response);

        await SendNoContentAsync(ct);
    }
}
