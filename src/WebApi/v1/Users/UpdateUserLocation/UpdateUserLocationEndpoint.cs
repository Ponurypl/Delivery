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
        //poprzedni zapis generował w dokumentacji swaggerowej konieczność uzupełnienia tego pola mimo tego że z niego nie korzystamy,
        //tylko dlatego że jest częścią śceiżki, teraz co gorsze mieć coś podane co i tak ignorujemy i można przez to źle zinterpretować dokumentację
        //myśląc że da się geo innego użytkownika nadpisać, czy mieć "niespójną" z innymi endpointami ścieżkę w użytkownikach?
        Put("location");
        Group<UsersEndpointGroup>();
        Version(1);
    }

    public override async Task HandleAsync(UpdateUserLocationRequest req, CancellationToken ct)
    {
        string? userId = User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value;
        if (userId is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        ErrorOr<Success> response = await _sender.Send(new UpdateUserLocationCommand() 
                                                      { 
                                                        UserId = Guid.Parse(userId!),
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
