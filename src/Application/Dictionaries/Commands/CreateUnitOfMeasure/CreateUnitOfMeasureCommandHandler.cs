using MediatR;
using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Dictionaries.Entities;

namespace MultiProject.Delivery.Application.Dictionaries.Commands.CreateUnitOfMeasure;

public sealed class CreateUnitOfMeasureCommandHandler : IHandler<CreateUnitOfMeasureCommand>
{
    private readonly IUnitOfMeasureRepository _unitOfMeasureRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUnitOfMeasureCommandHandler(IUnitOfWork unitOfWork, IUnitOfMeasureRepository unitOfMeasureRepository)
    {
        _unitOfWork = unitOfWork;
        _unitOfMeasureRepository = unitOfMeasureRepository;
    }

    public async Task<Unit> Handle(CreateUnitOfMeasureCommand request, CancellationToken cancellationToken)
    {
        UnitOfMeasure newUnitOfMeasure = new()
        {
            Name = request.Name,
            Description = request.Description,
            Symbol = request.Symbol
        };

        _unitOfMeasureRepository.Add(newUnitOfMeasure);
        await _unitOfWork.SaveChangesAsync();        

        return Unit.Value;
    }
}
