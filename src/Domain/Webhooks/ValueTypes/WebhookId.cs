using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Webhooks.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Guid, converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct WebhookId {}