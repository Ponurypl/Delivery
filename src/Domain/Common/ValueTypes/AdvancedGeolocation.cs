namespace MultiProject.Delivery.Domain.Common.ValueTypes;

public sealed class AdvancedGeolocation : Geolocation
{
    public DateTime LastUpdateDate { get; private set; }
    public double Heading { get; private set; }
    public double Speed { get; private set; }

    public AdvancedGeolocation(double latitude, double longitude, double accuracy, DateTime lastUpdateDate,
                               double heading, double speed)
        : base(latitude, longitude, accuracy)
    {
        LastUpdateDate = lastUpdateDate;
        Heading = heading;
        Speed = speed;
    }

    public static ErrorOr<AdvancedGeolocation> Create(double latitude, double longitude, double accuracy,
                                                      DateTime lastUpdateDate, double heading, double speed)
    {
        if (heading < 0 || speed < 0 || accuracy <= 0)
        {
            return DomainFailures.Geolocations.InvalidAdvancedGeolocation;
        }

        return new AdvancedGeolocation(latitude, longitude, accuracy, lastUpdateDate,
                                       heading, speed);
    }
}
