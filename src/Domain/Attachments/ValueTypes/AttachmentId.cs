using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Attachments.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct AttachmentId
{
}