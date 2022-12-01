using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Users.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct UserId {}