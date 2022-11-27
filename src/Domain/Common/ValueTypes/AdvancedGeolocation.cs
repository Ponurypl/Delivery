namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public sealed class AdvancedGeolocation : Geolocation
{
    public DateTime ReadDate { get; private set; }
    public double Heading { get; private set; }
    public double Speed { get; private set; }

    public AdvancedGeolocation(double latitude, double longitude, double accuracy, DateTime readDate,
                               double heading, double speed)
        : base(latitude, longitude, accuracy)
    {
        ReadDate = readDate;
        Heading = heading;
        Speed = speed;
    }

    public static ErrorOr<AdvancedGeolocation> Create(double latitude, double longitude, double accuracy,
                                                      DateTime readDate, double heading, double speed)
    {
        if (heading < 0 || speed < 0)
        {
            return DomainFailures.Geolocations.InvalidAdvancedGeolocation;
        }

        return new AdvancedGeolocation(latitude, longitude, accuracy, readDate,
                                       heading, speed);
    }
}
