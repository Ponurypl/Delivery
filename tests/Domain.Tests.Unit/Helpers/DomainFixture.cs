﻿using MultiProject.Delivery.Domain.Attachments.Entities;
using MultiProject.Delivery.Domain.Attachments.ValueTypes;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Deliveries.ValueTypes;
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
        public static UserId GetId => new(Guid.Parse("ec731cb6-0616-401b-8a81-ea13b60bfa05"));
        public static UserId GetRandomId => new(Guid.NewGuid());
        public static UserId GetEmptyId => UserId.Empty;

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

        public static Scan GetScan(IDateTime dateTime) => Scan.Create(TransportUnits.GetId, Users.GetId, dateTime).Value;
    }

    public static class Attachments
    {
        public static AttachmentId GetId => new(1);
        public static AttachmentId GetRandomId => new(Random.Shared.Next(1, 100));
        public static AttachmentId GetEmptyId => AttachmentId.Empty;
        public static Attachment GetAttachment()
        {
            IDateTime dateTime = Substitute.For<IDateTime>();
            return GetAttachment(dateTime);
        }
        public static Attachment GetAttachment(IDateTime dateTime) => Attachment.Create(Users.GetId, Transports.GetId, new byte[] { 54, 4, 3}, "abc", dateTime).Value;

    }

    public static class TransportUnits
    {
        public static TransportUnitId GetId => new(1);
        public static TransportUnitId GetRandomId => new(Random.Shared.Next(1, 100));
        public static TransportUnitId GetEmptyId => TransportUnitId.Empty;
    }

    public static class Transports
    {
        public static TransportId GetId => new(1);
        public static TransportId GetRandomId => new(Random.Shared.Next(1, 100));
        public static TransportId GetEmptyId => TransportId.Empty;
    }
}