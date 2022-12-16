using ErrorOr;
using MediatR;
using MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

namespace WebApi.v1.Users.UpdateUserLocation;

public class UpdateUserLocationEndpoint : Endpoint<UpdateUserLocationRequest>
{
    private readonly ISender _sender;

    public UpdateUserLocationEndpoint(ISender sender)
    {
        _sender = sender;
    }

    public override void Configure()
    {
        Put("users");
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
                                                      },ct);
        if (response.IsError)
        {
            foreach (var error in response.Errors)
            {
                AddError(error.Description, error.Code);
            }

            ThrowError("ErrorList");
            return;
        }

        await SendNoContentAsync();
    }
}
