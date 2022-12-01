using StronglyTypedIds;

namespace MultiProject.Delivery.Domain.Scans.ValueTypes;

[StronglyTypedId(backingType: StronglyTypedIdBackingType.Int, StronglyTypedIdConverter.SystemTextJson | StronglyTypedIdConverter.EfCoreValueConverter)]
public partial struct ScanId
{
    
}