using MultiProject.Delivery.Domain.Common.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class LocationFixtures : BaseFixture
    {
        private readonly Faker<AdvancedGeolocation> _advancedGeolocationFaker;
        public LocationFixtures(DomainFixture fixture) : base(fixture)
        {
            _advancedGeolocationFaker = new Faker<AdvancedGeolocation>()
                                       .UseSeed(369345581)
                                       .CustomInstantiator(_ => DomainObjectBuilder.Create<AdvancedGeolocation>())
                                       .RuleFor(x => x.Latitude, f => f.Random.Double())
                                       .RuleFor(x => x.Longitude, f => f.Random.Double())
                                       .RuleFor(x => x.Heading, f => f.Random.Double())
                                       .RuleFor(x => x.LastUpdateDate, f => f.Date.Recent())
                                       .RuleFor(x => x.Speed, f => f.Random.Double())
                                       .RuleFor(x => x.Accuracy, f => f.Random.Double());
        }

        public AdvancedGeolocation GetAdvancedGeolocation() => _advancedGeolocationFaker.Generate();
    }
}