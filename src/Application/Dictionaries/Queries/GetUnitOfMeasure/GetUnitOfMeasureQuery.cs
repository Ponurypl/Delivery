namespace MultiProject.Delivery.Application.Dictionaries.Queries.GetUnitOfMeasure;
public sealed record GetUnitOfMeasureQuery : IQuery<GetUnitOfMeasureDto>
{
    public required int Id { get; init; }
}
