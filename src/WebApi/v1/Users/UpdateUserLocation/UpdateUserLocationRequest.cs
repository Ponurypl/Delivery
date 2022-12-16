namespace MultiProject.Delivery.WebApi.v1.Users.UpdateUserLocation;

public class UpdateUserLocationRequest
{
    public Guid UserId { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public double Accuracy { get; init; }
    public double Heading { get; init; }
    public double Speed { get; init; }
}
