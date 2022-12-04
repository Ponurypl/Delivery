namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

public sealed record UpdateUserLocationCommand : ICommand
{
    public required Guid UserId { get; init; }
    public required double Latitude { get; init; }
    public required double Longitude { get; init; }
    public required double Accuracy { get; init; }
    public required double Heading { get; init; }
    public required double Speed { get; init; }
}
