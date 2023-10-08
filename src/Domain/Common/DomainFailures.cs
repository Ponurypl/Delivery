namespace MultiProject.Delivery.Domain.Common;

internal static class DomainFailures
{
    public static class Common
    {
        public static Error MissingRequiredDependency => Error.Unexpected(nameof(MissingRequiredDependency));
        public static Error MissingParentObject => Error.Validation(nameof(MissingParentObject));
        public static Error MissingChildObject => Error.Validation(nameof(MissingChildObject));
    }
    
    public static class Geolocations
    {
        public static Error InvalidGeolocation => Error.Validation(nameof(InvalidGeolocation));
        public static Error InvalidAdvancedGeolocation => Error.Validation(nameof(InvalidAdvancedGeolocation));
    }

    public static class Deliveries
    {
        public static Error InvalidUniqueUnitDetails => Error.Validation(nameof(InvalidUniqueUnitDetails));
        public static Error InvalidMultiUnitDetails => Error.Validation(nameof(InvalidMultiUnitDetails));
        public static Error InvalidTransportUnit => Error.Validation(nameof(InvalidTransportUnit));
        public static Error InvalidTransport => Error.Validation(nameof(InvalidTransport));
        public static Error InvalidTransportUnitDetails => Error.Validation(nameof(InvalidTransportUnitDetails));
        public static Error InvalidRecipient => Error.Validation(nameof(InvalidRecipient));
        public static Error WrongTransportUnitStatus => Error.Conflict(nameof(WrongTransportUnitStatus));
        public static Error WrongTransportStatus => Error.Conflict(nameof(WrongTransportStatus));
    }

    public static class Dictionaries
    {
        public static Error InvalidUnitOfMeasure => Error.Validation(nameof(InvalidUnitOfMeasure));
    }

    public static class Scans
    {
        public static Error InvalidScan => Error.Validation(nameof(InvalidScan));
    }

    public static class Users
    {
        public static Error UserDoesNotMeetRole => Error.Conflict(nameof(UserDoesNotMeetRole));
        public static Error InvalidUser => Error.Validation(nameof(InvalidUser));
    }

    public static class Attachments
    {
        public static Error InvalidAttachment => Error.Validation(nameof(InvalidAttachment));
    }

    public class Webhooks
    {
        public static Error InvalidWebhook => Error.Validation(nameof(InvalidWebhook));
    }
}