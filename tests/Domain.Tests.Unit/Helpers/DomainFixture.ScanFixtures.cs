using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class ScanFixtures : BaseFixture
    {
        private readonly Faker<Scan> _scanFaker;

        public ScanFixtures(DomainFixture fixture) : base(fixture)
        {
            _scanFaker = new Faker<Scan>()
                         .UseSeed(1478454517)
                         .CustomInstantiator(_ => DomainObjectBuilder.Create<Scan, ScanId>(GetId))
                         .RuleFor(x => x.TransportUnitId, Fixture.TransportUnits.GetId)
                         .RuleFor(x => x.LastUpdateDate, f => f.Date.Recent());
        }

        public ScanId GetId() => new(Faker.Random.Int(1, 100));

        public Scan GetScan() => _scanFaker.Generate();
    }
}