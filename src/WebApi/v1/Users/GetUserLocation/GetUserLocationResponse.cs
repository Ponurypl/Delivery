namespace MultiProject.Delivery.WebApi.v1.Users.GetUserLocation;

public sealed record GetUserLocationResponse
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double Accuracy { get; set; }
    public double Heading { get; set; }
    public double Speed { get; set; }
    public DateTime LastUpdateDate { get; set; }
}
