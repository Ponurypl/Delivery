using Bogus;
using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Entities;
using MultiProject.Delivery.Domain.Deliveries.Enums;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
using MultiProject.Delivery.Domain.Dictionaries.Entities;
using MultiProject.Delivery.Domain.Dictionaries.ValueTypes;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

public static class DomainFixture
{
    private static readonly Faker _faker = new();

    static DomainFixture()
    {
        Randomizer.Seed = new Random(78956987);
    }

    public class Users
    {
        public static readonly string Username = _faker.Internet.UserName();
        public static readonly string Password = _faker.Internet.Password();
        public static readonly string PhoneNumber = _faker.Phone.PhoneNumber();

        public static UserId GetId() => new(_faker.Random.Guid());

        public static User GetUser(UserRole role = UserRole.Deliverer)
            => new Faker<User>()
               .CustomInstantiator(_ => EntityBuilder.Create<User, UserId>(GetId))
               .RuleFor(x => x.Username, f => f.Internet.UserName())
               .RuleFor(x => x.Password, f => f.Internet.Password())
               .RuleFor(x => x.PhoneNumber, f => f.Phone.PhoneNumber())
               .RuleFor(x => x.Role, role)
               .RuleFor(x => x.IsActive, true)
               .Generate();
    }

    public static class Scans
    {
        public static ScanId GetId() => new(_faker.Random.Int(1,100));

        public static Scan GetScan()
            => new Faker<Scan>()
               .CustomInstantiator(_ => EntityBuilder.Create<Scan, ScanId>(GetId))
               .RuleFor(x => x.TransportUnitId, TransportUnits.GetId)
               .RuleFor(x => x.LastUpdateDate, f => f.Date.Recent())
               .Generate();
    }

    public static class Attachments
    {
        public static readonly byte[] Payload = _faker.Random.Bytes(50);
        public static readonly string AdditionalInformation = _faker.Lorem.Sentence();

        public static AttachmentId GetId() => new(_faker.Random.Int(1,100));

        public static Attachment GetAttachment()
            => new Faker<Attachment>()
               .CustomInstantiator(_ => EntityBuilder.Create<Attachment, AttachmentId>(GetId))
               .RuleFor(x => x.CreatorId, Users.GetId)
               .RuleFor(x => x.TransportId, Transports.GetId)
               .RuleFor(x => x.Payload, f => f.Random.Bytes(50))
               .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
               .RuleFor(x => x.LastUpdateDate, f => f.Date.Recent())
               .Generate();
    }

    public static class UnitOfMeasures
    {
        private static readonly string[] _names = { "Kilogram", "Ton", "Liter", "Cubic Meter" };
        private static readonly string[] _symbols = { "Kg", "t", "l", "m3" };

        public static readonly string Name = _faker.PickRandom(_names);
        public static readonly string Symbol = _faker.PickRandom(_symbols);
        public static readonly string Description = _faker.Lorem.Sentence();

        public static UnitOfMeasureId GetId() => new(_faker.Random.Int(1,100));

        public static UnitOfMeasure GetUnitOfMeasure()
            => new Faker<UnitOfMeasure>()
               .CustomInstantiator(_ => EntityBuilder.Create<UnitOfMeasure, UnitOfMeasureId>(GetId))
               .RuleFor(x => x.Name, f => f.PickRandom(_names))
               .RuleFor(x => x.Symbol, f => f.PickRandom(_symbols))
               .RuleFor(x => x.Description, f => f.Lorem.Sentence())
               .Generate();
    }

    public static class TransportUnits
    {
        public static readonly string Number = _faker.Random.Replace("?? ####/##/####/?");
        public static readonly string AdditionalInformation = _faker.Lorem.Sentence();
        public static readonly string Description = _faker.Lorem.Sentence();
        public static readonly double Amount = _faker.Random.Double(1, 200);
        public static readonly string Barcode = _faker.Commerce.Ean13();

        public static TransportUnitId GetId() => new(_faker.Random.Int(1, 100));

        public static TransportUnit GetTransportUnit()
            => new Faker<TransportUnit>()
               .CustomInstantiator(_ => EntityBuilder.Create<TransportUnit, TransportUnitId>(GetId))
               .RuleFor(x => x.Number, f => f.Random.Replace("?? ####/##/####/?"))
               .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
               .RuleFor(x => x.Description, f => f.Lorem.Sentence())
               .RuleFor(x => x.Status, TransportUnitStatus.New)
               .RuleFor(x => x.Recipient, Recipients.GetRecipient)
               .Generate();

    }

    public static class Transports
    {
        public static readonly string Number = _faker.Random.Replace("?? ####/##/####/?");
        public static readonly string AdditionalInformation = _faker.Lorem.Sentence();
        public static readonly double TotalWeight = _faker.Random.Double(1, 50);

        public static TransportId GetId() => new(_faker.Random.Int(1,100));

        public static Transport GetTransport()
            => new Faker<Transport>()
               .CustomInstantiator(_ => EntityBuilder.Create<Transport, TransportId>(GetId))
               .RuleFor(x => x.DelivererId, Users.GetId)
               .RuleFor(x => x.Number, f => f.Random.Replace("?? ####/##/####/?"))
               .RuleFor(x => x.AdditionalInformation, f => f.Lorem.Sentence())
               .RuleFor(x => x.TotalWeight, f => f.Random.Double(1, 50))
               .RuleFor(x => x.StartDate, f => f.Date.Soon())
               .RuleFor(x => x.ManagerId, Users.GetId)
               .RuleFor("_transportUnits", f => f.Make(6, TransportUnits.GetTransportUnit))
               .Generate();

        public static Transport GetEmptyTransport() => EntityBuilder.Create<Transport, TransportId>(GetId());

        public static NewTransportUnit GetUniqueTransportUnitDto(string? number = null) =>
            new Faker<NewTransportUnit>()
                .Rules((f, o) =>
                       {
                           o.Description = f.Lorem.Sentence();
                           o.Number = number ?? f.Random.Replace("?? ####/##/####/?");
                           o.AdditionalInformation = f.Lorem.Sentence();
                           o.RecipientCompanyName = f.Company.CompanyName();
                           o.RecipientName = f.Person.FirstName;
                           o.RecipientLastName = f.Person.LastName;
                           o.RecipientPhoneNumber = f.Phone.PhoneNumber();
                           o.RecipientFlatNumber = f.Address.BuildingNumber();
                           o.RecipientStreetNumber = f.Address.BuildingNumber();
                           o.RecipientStreet = f.Address.StreetName();
                           o.RecipientTown = f.Address.City();
                           o.RecipientCountry = f.Address.Country();
                           o.RecipientPostCode = f.Address.ZipCode();
                           o.Barcode = f.Commerce.Ean13();
                       })
                .Generate();

        public static NewTransportUnit GetMultiTransportUnitDto(string? number = null) =>
            new Faker<NewTransportUnit>()
                .Rules((f, o) =>
                       {
                           o.Description = f.Lorem.Sentence();
                           o.Number = number ?? f.Random.Replace("?? ####/##/####/?");
                           o.AdditionalInformation = f.Lorem.Sentence();
                           o.RecipientCompanyName = f.Company.CompanyName();
                           o.RecipientName = f.Person.FirstName;
                           o.RecipientLastName = f.Person.LastName;
                           o.RecipientPhoneNumber = f.Phone.PhoneNumber();
                           o.RecipientFlatNumber = f.Address.BuildingNumber();
                           o.RecipientStreetNumber = f.Address.BuildingNumber();
                           o.RecipientStreet = f.Address.StreetName();
                           o.RecipientTown = f.Address.City();
                           o.RecipientCountry = f.Address.Country();
                           o.RecipientPostCode = f.Address.ZipCode();
                           o.UnitOfMeasureId = f.Random.Number(10);
                           o.Amount = f.Random.Double(10, 50);
                       })
                .Generate();

        public static List<NewTransportUnit> GetTransportUnitsDtos()
            => Enumerable.Range(0, 5)
                         .SelectMany(_ => new[] { GetMultiTransportUnitDto(), GetUniqueTransportUnitDto() })
                         .ToList();
    }

    public static class Recipients
    {
        public static readonly string CompanyName = "companyName";
        public static readonly string Country = "country";
        public static readonly string FlatNumber = "flatNumber";
        public static readonly string LastName = "lastname";
        public static readonly string Name = "name";
        public static readonly string PhoneNumber = "phoneNumber";
        public static readonly string PostCode = "postCode";
        public static readonly string Street = "street";
        public static readonly string StreetNumber = "streetNumber";
        public static readonly string Town = "town";
        public static Recipient GetRecipient()
        {
            return Recipient.Create(CompanyName, Country, FlatNumber, LastName, Name, PhoneNumber, PostCode, Street, StreetNumber, Town).Value;
        }
    }
}