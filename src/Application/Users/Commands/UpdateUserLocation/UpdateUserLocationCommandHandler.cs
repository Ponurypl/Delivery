using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Common.DateTimeProvider;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocation;

public sealed class UpdateUserLocationCommandHandler : ICommandHandler<UpdateUserLocationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IDateTime _dateTime;

    public UpdateUserLocationCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IDateTime dateTime)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _dateTime = dateTime;
    }

    public async Task<ErrorOr<Success>> Handle(UpdateUserLocationCommand request, CancellationToken cancellationToken)
    {
        var updatedUser = await _userRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);
        if (updatedUser is null) return Failure.UserNotExists;

        updatedUser.UpdateGeolocation(request.Latitude, request.Longitude, request.Accuracy, request.Heading,
                                      request.Speed, _dateTime.Now);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Success;
    }
}
