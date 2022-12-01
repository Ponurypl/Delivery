﻿using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Deliveries.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct TransportId {}
