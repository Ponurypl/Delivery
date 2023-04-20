using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class TransportFixtures : BaseFixture
    {
        private readonly Faker<NewTransportUnit> _uniqueUnitFaker;
        private readonly Faker<NewTransportUnit> _multiUnitFaker;
        private readonly Faker<Transport> _transportFaker;

        public TransportFixtures(DomainFixture fixture) : base(fixture)
        {
            _uniqueUnitFaker = new Faker<NewTransportUnit>()
                               .UseSeed(6587895)
                               .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                               .RuleFor(x => x.Number, f => f.Random.Replace("?? ####/##/####/?"))
                               .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
                               .RuleFor(x => x.RecipientCompanyName, f => f.Company.CompanyName())
                               .RuleFor(x => x.RecipientName, f => f.Person.FirstName)
                               .RuleFor(x => x.RecipientLastName, f => f.Person.LastName)
                               .RuleFor(x => x.RecipientPhoneNumber, f => f.Phone.PhoneNumber())
                               .RuleFor(x => x.RecipientFlatNumber, f => f.Address.BuildingNumber())
                               .RuleFor(x => x.RecipientStreetNumber, f => f.Address.BuildingNumber())
                               .RuleFor(x => x.RecipientStreet, f => f.Address.StreetName())
                               .RuleFor(x => x.RecipientTown, f => f.Address.City())
                               .RuleFor(x => x.RecipientCountry, f => f.Address.Country())
                               .RuleFor(x => x.RecipientPostCode, f => f.Address.ZipCode())
                               .RuleFor(x => x.Barcode, f => f.Commerce.Ean13());

            _multiUnitFaker = new Faker<NewTransportUnit>()
                              .UseSeed(124587)
                              .RuleFor(x => x.Description, f => f.Lorem.Sentence())
                              .RuleFor(x => x.Number, f => f.Random.Replace("?? ####/##/####/?"))
                              .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
                              .RuleFor(x => x.RecipientCompanyName, f => f.Company.CompanyName())
                              .RuleFor(x => x.RecipientName, f => f.Person.FirstName)
                              .RuleFor(x => x.RecipientLastName, f => f.Person.LastName)
                              .RuleFor(x => x.RecipientPhoneNumber, f => f.Phone.PhoneNumber())
                              .RuleFor(x => x.RecipientFlatNumber, f => f.Address.BuildingNumber())
                              .RuleFor(x => x.RecipientStreetNumber, f => f.Address.BuildingNumber())
                              .RuleFor(x => x.RecipientStreet, f => f.Address.StreetName())
                              .RuleFor(x => x.RecipientTown, f => f.Address.City())
                              .RuleFor(x => x.RecipientCountry, f => f.Address.Country())
                              .RuleFor(x => x.RecipientPostCode, f => f.Address.ZipCode())
                              .RuleFor(x => x.UnitOfMeasureId, f => f.Random.Number(1, 10))
                              .RuleFor(x => x.Amount, f => f.Random.Double(10, 50));

            _transportFaker = new Faker<Transport>()
                              .UseSeed(3692581)
                              .CustomInstantiator(_ => DomainObjectBuilder.Create<Transport, TransportId>(GetId))
                              .RuleFor(x => x.DelivererId, Fixture.Users.GetId)
                              .RuleFor(x => x.Number, f => f.Random.Replace("?? ####/##/####/?"))
                              .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
                              .RuleFor(x => x.TotalWeight, f => f.Random.Double(1, 50))
                              .RuleFor(x => x.StartDate, f => f.Date.Soon())
                              .RuleFor(x => x.ManagerId, Fixture.Users.GetId);
            //.RuleFor("_transportUnits", f => f.Make(6, TransportUnits.GetTransportUnit));
        }

        public string Number => Faker.Random.Replace("?? ####/##/####/?");
        public string AdditionalInformation => Faker.Lorem.Sentence();
        public double TotalWeight => Faker.Random.Double(1, 50);


        public TransportId GetId() => new(Faker.Random.Int(1, 100));

        public Transport GetTransport() => _transportFaker.Generate();

        public Transport GetEmptyTransport() => DomainObjectBuilder.Create<Transport, TransportId>(GetId());

        public NewTransportUnit GetUniqueTransportUnitDto(string? number = null)
        {
            NewTransportUnit transport = _uniqueUnitFaker.Generate();
            if (number is not null)
            {
                transport.Number = number;
            }

            return transport;
        }

        public NewTransportUnit GetMultiTransportUnitDto(string? number = null)
        {
            NewTransportUnit transport = _multiUnitFaker.Generate();
            if (number is not null)
            {
                transport.Number = number;
            }

            return transport;
        }

        public List<NewTransportUnit> GetMixedTransportUnitsDto(int count = 10)
        {
            List<NewTransportUnit> list = new();
            int uniqueCount = (int)Math.Floor(count / 2d);
            list.AddRange(_uniqueUnitFaker.Generate(uniqueCount));
            list.AddRange(_multiUnitFaker.Generate(count - uniqueCount));

            return list.OrderBy(_ => Faker.Random.Guid()).ToList();
        }
    }
}