using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Deliveries.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct TransportUnitId { }
