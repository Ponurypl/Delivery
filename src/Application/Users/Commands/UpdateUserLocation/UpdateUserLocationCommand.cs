namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

public sealed record UpdateUserLocationCommand : ICommand
{
    public Guid UserId { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
    public double Heading { get; init; }
    public double Speed { get; init; }
}
