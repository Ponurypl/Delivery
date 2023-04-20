using MultiProject.Delivery.Domain.Deliveries.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public partial class DomainFixture
{
    public class RecipientFixtures : BaseFixture
    {
        private readonly Faker<Recipient> _recipientFaker;

        public RecipientFixtures(DomainFixture fixture) : base(fixture)
        {
            _recipientFaker = new Faker<Recipient>()
                              .UseSeed(3692581)
                              .CustomInstantiator(_ => DomainObjectBuilder.Create<Recipient>())
                              .RuleFor(x => x.CompanyName, f => f.Company.CompanyName())
                              .RuleFor(x => x.Country, f => f.Address.Country())
                              .RuleFor(x => x.FlatNumber, f => f.Address.BuildingNumber())
                              .RuleFor(x => x.LastName, f => f.Person.LastName)
                              .RuleFor(x => x.Name, f => f.Person.FirstName)
                              .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
                              .RuleFor(x => x.PostCode, f => f.Address.ZipCode())
                              .RuleFor(x => x.Street, f => f.Address.StreetName())
                              .RuleFor(x => x.StreetNumber, f => f.Address.BuildingNumber())
                              .RuleFor(x => x.Town, f => f.Address.City());
        }

        public string CompanyName => Faker.Company.CompanyName();
        public string Country => Faker.Address.Country();
        public string FlatNumber => Faker.Address.BuildingNumber();
        public string LastName => Faker.Person.LastName;
        public string Name => Faker.Person.FirstName;
        public string PhoneNumber => Faker.Phone.PhoneNumber();
        public string PostCode => Faker.Address.ZipCode();
        public string Street => Faker.Address.StreetName();
        public string StreetNumber => Faker.Address.BuildingNumber();
        public string Town => Faker.Address.City();

        public Recipient GetRecipient() => _recipientFaker.Generate();
    }
}