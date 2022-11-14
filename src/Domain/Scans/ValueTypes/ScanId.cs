using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Scans.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, StronglyTypedIdConverter.SystemTextJson)]
public partial struct ScanId
{
    
}