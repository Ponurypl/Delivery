using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Users.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct UserId {}