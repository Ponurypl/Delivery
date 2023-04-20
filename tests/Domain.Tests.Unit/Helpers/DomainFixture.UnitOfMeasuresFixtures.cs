using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class UnitOfMeasuresFixtures : BaseFixture
    {
        private readonly Faker<UnitOfMeasure> _unitOfMeasureFaker;

        public UnitOfMeasuresFixtures(DomainFixture fixture) : base(fixture)
        {
            _unitOfMeasureFaker = new Faker<UnitOfMeasure>()
                                  .UseSeed(876514424)
                                  .CustomInstantiator(_ => DomainObjectBuilder.Create<UnitOfMeasure, UnitOfMeasureId>(GetId))
                                  .RuleFor(x => x.Name, f => f.PickRandom(_names))
                                  .RuleFor(x => x.Symbol, f => f.PickRandom(_symbols))
                                  .RuleFor(x => x.Description, f => f.Lorem.Sentence());
        }

        private readonly string[] _names = { "Kilogram", "Ton", "Liter", "Cubic Meter" };
        private readonly string[] _symbols = { "Kg", "t", "l", "m3" };

        public string Name => Faker.PickRandom(_names);
        public string Symbol => Faker.PickRandom(_symbols);
        public string Description => Faker.Lorem.Sentence();

        public UnitOfMeasureId GetId() => new(Faker.Random.Int(1, 100));

        public UnitOfMeasure GetUnitOfMeasure() => _unitOfMeasureFaker.Generate();
    }
}