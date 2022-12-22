using MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;

namespace MultiProject.Delivery.WebApi.v1.Dictionaries.GetUnitOfMeasure;

public sealed class Mapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GetUnitOfMeasureDto, GetUnitOfMeasureResponse>();
    }
}
