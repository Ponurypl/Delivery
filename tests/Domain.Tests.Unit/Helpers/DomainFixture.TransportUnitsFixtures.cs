using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class TransportUnitsFixtures : BaseFixture
    {
        private readonly Faker<TransportUnit> _transportUnitFaker;

        public TransportUnitsFixtures(DomainFixture fixture) : base(fixture)
        {
            _transportUnitFaker = new Faker<TransportUnit>()
                                  .UseSeed(454654961)
                                  .CustomInstantiator(_ => DomainObjectBuilder.Create<TransportUnit, TransportUnitId>(GetId))
                                  .RuleFor(x => x.Number, f => f.Random.Replace("?? ####/##/####/?"))
                                  .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
                                  .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                                  .RuleFor(x => x.Status, TransportUnitStatus.New)
                                  .RuleFor(x => x.Recipient, Fixture.Recipients.GetRecipient);
        }

        public string Number => Faker.Random.Replace("?? ####/##/####/?");
        public string AdditionalInformation => Faker.Lorem.Sentence();
        public string Description => Faker.Lorem.Sentence();
        public double Amount => Faker.Random.Double(1, 200);
        public string Barcode => Faker.Commerce.Ean13();


        public TransportUnitId GetId() => new(Faker.Random.Int(1, 100));

        public TransportUnit GetTransportUnit() => _transportUnitFaker.Generate();
    }
}