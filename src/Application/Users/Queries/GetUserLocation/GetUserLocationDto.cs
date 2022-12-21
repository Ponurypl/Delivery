namespace MultiProject.Delivery.Application.Users.Queries.GetUserLocation;

public sealed record GetUserLocationDto
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Accuracy { get; set; }
    public double Heading { get; set; }
    public double Speed { get; set; }
    public DateTime LastUpdateDate { get; set; }
}