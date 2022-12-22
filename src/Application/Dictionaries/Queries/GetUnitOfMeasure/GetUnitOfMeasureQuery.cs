namespace MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;
public sealed record GetUnitOfMeasureQuery : IQuery<GetUnitOfMeasureDto>
{
    public int Id { get; init; }
}
