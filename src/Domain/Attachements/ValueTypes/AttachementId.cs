using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Attachements.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct AttachementId
{
}