using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Scans.Entities;
using MultiProject.Delivery.Domain.Scans.ValueTypes;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Enums;
using MultiProject.Delivery.Domain.Users.ValueTypes;
using System;

namespace MultiProject.Delivery.Domain.Tests.Unit.Helpers;

internal static class DomainFixture
{
    public static class Users
    {
        public static UserId GetId() => new(Guid.Parse("ec731cb6-0616-401b-8a81-ea13b60bfa05"));
        public static UserId GetRandomId() => new(Guid.NewGuid());
        public static UserId GetEmptyId() => UserId.Empty;

        public static User GetUser(UserRole role = UserRole.Deliverer) =>
            User.Create(role, "SampleUsername", "password@#$hash!!!", "1234567890").Value;
    }

    public static class Scans
    {
        public static ScanId GetId => new(1);
        public static ScanId GetRandomId => new(Random.Shared.Next(1,100));
        public static ScanId GetEmptyId => ScanId.Empty;

        public static Scan GetScan()
        {
            IDateTime dateTime = Substitute.For<IDateTime>();
            return GetScan(dateTime);
        }

        public static Scan GetScan(IDateTime dateTime) => Scan.Create(new(1), Users.GetId(), dateTime).Value; //TODO: Zmienić id transportu
                                                                                                              //TODO: P.S. chuju wiesz o co mi chodzi
    }
}