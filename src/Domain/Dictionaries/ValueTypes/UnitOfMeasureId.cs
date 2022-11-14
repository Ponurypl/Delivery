using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct UnitOfMeasureId { }