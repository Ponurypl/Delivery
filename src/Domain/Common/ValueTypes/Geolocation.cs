using MultiProject.Delivery.Domain.Common.Abstractions;

namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public class Geolocation : ValueObject
{
    public double Latitude { get; private set; }
    public double Longitude { get; private set; }
    public double Accuracy { get; private set; }

#pragma warning disable CS8618, IDE0051
    protected Geolocation() { }
#pragma warning restore 

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

    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Latitude;
        yield return Longitude;
        yield return Accuracy;
    }
}
