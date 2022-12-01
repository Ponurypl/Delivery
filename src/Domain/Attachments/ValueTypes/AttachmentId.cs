using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Attachments.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct AttachmentId
{
}