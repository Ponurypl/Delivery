using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Dictionaries.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct UnitOfMeasureId { }