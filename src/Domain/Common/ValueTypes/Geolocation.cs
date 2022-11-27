namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public class Geolocation
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public double Accuracy { get; private set; }

    protected Geolocation(double latitude, double longitude, double accuracy)
    {
        Latitude = latitude;
        Longitude = longitude;
        Accuracy = accuracy;
    }

    public static ErrorOr<Geolocation> Create(double latitude, double longitude, double accuracy)
    {
        if (accuracy <= 0)
        {
            return DomainFailures.Geolocations.InvalidGeolocation;
        }

        return new Geolocation(latitude, longitude, accuracy);
    }
}
