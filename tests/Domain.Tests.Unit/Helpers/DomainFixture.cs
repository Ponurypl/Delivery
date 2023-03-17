using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.DTO;
using MultiProject.Delivery.Domain.Deliveries.Entities;
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
    public class Users
    {
        public static readonly string Username = "SampleUsername";
        public static readonly string Password = "password@#$hash!!!";
        public static readonly string PhoneNumber = "1234567890";

        public static UserId GetId() => new(Guid.Parse("ec731cb6-0616-401b-8a81-ea13b60bfa05"));
        public static UserId GetRandomId() => new(Guid.NewGuid());
        public static UserId GetEmptyId() => UserId.Empty;

        public static User GetUser(UserRole role = UserRole.Deliverer)
        {
            var user = EntityBuilder.Create<User, UserId>(GetId());
            user.Set(x => x.IsActive, true);
            user.Set(x => x.Role, role);
            user.Set(x => x.Username, Username);
            user.Set(x => x.Password, Password);
            user.Set(x => x.PhoneNumber, PhoneNumber);

            return user;
        }
    }

    public static class Scans
    {
        public static ScanId GetId() => new(1);
        public static ScanId GetRandomId() => new(Random.Shared.Next(1,100));
        public static ScanId GetEmptyId() => ScanId.Empty;

        public static Scan GetScan()
        {
            IDateTime dateTime = Substitute.For<IDateTime>();
            return GetScan(dateTime);
        }

        public static Scan GetScan(IDateTime dateTime) => Scan.Create(TransportUnits.GetId(), Users.GetId(), dateTime).Value;
    }

    public static class Attachments
    {
        public static readonly byte[] Payload = { 54, 4, 3 };
        public static readonly string AdditionalInformation = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

        public static AttachmentId GetId() => new(1);
        public static AttachmentId GetRandomId() => new(Random.Shared.Next(1, 100));
        public static AttachmentId GetEmptyId() => AttachmentId.Empty;
        public static Attachment GetAttachment()
        {
            IDateTime dateTime = Substitute.For<IDateTime>();
            return GetAttachment(dateTime);
        }

        public static Attachment GetAttachment(IDateTime dateTime)
        {
            return Attachment.Create(Users.GetId(), Transports.GetId(), Payload, AdditionalInformation, dateTime).Value;
        }
    }

    public static class UnitOfMeasures
    {
        public static readonly string Name = "Kilogram";
        public static readonly string Symbol = "Kg";
        public static readonly string Description = "Sample description";

        public static UnitOfMeasureId GetId() => new(1);
        public static UnitOfMeasureId GetRandomId() => new(Random.Shared.Next(1, 100));
        public static UnitOfMeasureId GetEmptyId() => UnitOfMeasureId.Empty;

        public static UnitOfMeasure GetUnitOfMeasure()
        {
            var unit = EntityBuilder.Create<UnitOfMeasure, UnitOfMeasureId>(GetId());
            unit.Name = Name;
            unit.Symbol = Symbol;
            unit.Description = Description;

            return unit;
        }
    }

    public static class TransportUnits
    {
        public static readonly string Number = "number";
        public static readonly string AdditionalInformation = "additionalInformation";
        public static readonly string Description = "Description";
        public static readonly double Amount = 234.3D;
        public static readonly string Barcode = "Barcode1234";

        public static TransportUnitId GetId() => new(1);
        public static TransportUnitId GetRandomId() => new(Random.Shared.Next(1, 100));
        public static TransportUnitId GetEmptyId() => TransportUnitId.Empty;
        //public static TransportUnit GetTransportUnit() => TransportUnit.Create{}
    }

    public static class Transports
    {
        public static readonly string Number = "ABC123";
        public static readonly string AdditionalInformation = "asdsfFFSDC";
        public static readonly double TotalWeight = 34.34d;

        public static TransportId GetId() => new(1);
        public static TransportId GetRandomId() => new(Random.Shared.Next(1, 100));
        public static TransportId GetEmptyId() => TransportId.Empty;

        public static Transport GetTransport(IDateTime dateTime)
        {
            return Transport.Create(Users.GetRandomId(), Number, AdditionalInformation, TotalWeight,
                                    dateTime.UtcNow.AddHours(1), Users.GetRandomId(), dateTime,
                                    NewTransportUnits.GetFilledList()).Value;
        }

        public static Transport GetEmptyTransport()
        {
            return EntityBuilder.Create<Transport, TransportId>(GetId());
        }
    }

    public static class NewTransportUnits
    {
        public static List<NewTransportUnit> GetFilledList() => new() { GetPredefinedMultiTransportUnit(), GetPredefinedUniqueTransportUnit(), 
                                                                        GetRandomUniqueTransportUnit(),GetRandomUniqueTransportUnit(),
                                                                        GetRandomMultiTransportUnit(), GetRandomMultiTransportUnit()};
        public static List<NewTransportUnit> GetEmptyList() => new();
        public static NewTransportUnit GetPredefinedUniqueTransportUnit(string number = "DEF/4352/2245-223/dd", string barcode = "53465456453")
        {
            return new NewTransportUnit
                   {
                       Description = "Great Success",
                       Number = number,
                       AdditionalInformation = "1234ABCD",
                       RecipientCompanyName = "super name",
                       RecipientName = "Alberto",
                       RecipientLastName = "Gerat",
                       RecipientPhoneNumber = "505483544",
                       RecipientFlatNumber = "34",
                       RecipientStreetNumber = "23B-3",
                       RecipientStreet = "Striite",
                       RecipientTown = "London",
                       RecipientCountry = "Moon",
                       RecipientPostCode = "54-643",
                       Barcode = barcode
            };
        }

        public static NewTransportUnit GetPredefinedMultiTransportUnit(string number = "ABC/1244/2023-354/sd")
        {
            return new NewTransportUnit
                   {
                       Description = "Great Success",
                       Number = number,
                       AdditionalInformation = "1234ABCD",
                       RecipientCompanyName = "super name",
                       RecipientName = "Alberto",
                       RecipientLastName = "Gerat",
                       RecipientPhoneNumber = "505483544",
                       RecipientFlatNumber = "34",
                       RecipientStreetNumber = "23B-3",
                       RecipientStreet = "Striite",
                       RecipientTown = "London",
                       RecipientCountry = "Moon",
                       RecipientPostCode = "54-643",
                       UnitOfMeasureId = 12,
                       Amount = 34.53d
                   };
        }

        public static NewTransportUnit GetRandomUniqueTransportUnit(string? number = null, string? barcode = null)
        {
            return new NewTransportUnit
            {
                Description = "Great Success",
                Number = number ?? Guid.NewGuid().ToString(),
                AdditionalInformation = "1234ABCD",
                RecipientCompanyName = "super name",
                RecipientName = "Alberto",
                RecipientLastName = "Gerat",
                RecipientPhoneNumber = "505483544",
                RecipientFlatNumber = "34",
                RecipientStreetNumber = "23B-3",
                RecipientStreet = "Striite",
                RecipientTown = "London",
                RecipientCountry = "Moon",
                RecipientPostCode = "54-643",
                Barcode = barcode ?? Guid.NewGuid().ToString()
            };
        }

        public static NewTransportUnit GetRandomMultiTransportUnit(string? number = null)
        {
            return new NewTransportUnit
            {
                Description = "Great Success",
                Number = number ?? Guid.NewGuid().ToString(),
                AdditionalInformation = "1234ABCD",
                RecipientCompanyName = "super name",
                RecipientName = "Alberto",
                RecipientLastName = "Gerat",
                RecipientPhoneNumber = "505483544",
                RecipientFlatNumber = "34",
                RecipientStreetNumber = "23B-3",
                RecipientStreet = "Striite",
                RecipientTown = "London",
                RecipientCountry = "Moon",
                RecipientPostCode = "54-643",
                UnitOfMeasureId = 12,
                Amount = 34.53d
            };
        }
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