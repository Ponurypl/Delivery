using MediatR;
using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Common.Entities;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.Exceptions;

namespace MultiProject.Delivery.Application.Users.Commands.UpdateUserLocalization;

public sealed class UpdateUserLocalizationCommandHandler : IHandler<UpdateUserLocalizationCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IDateTime _dateTime;

    public UpdateUserLocalizationCommandHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IDateTime dateTime)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _dateTime = dateTime;
    }

    public async Task<Unit> Handle(UpdateUserLocalizationCommand request, CancellationToken cancellationToken)
    {
        AdvancedGeolocalization advancedGeolocalization = new()
        {
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Accuracy = request.Accuracy,
            GeoHeading = request.GeoHeading,
            GeoReadDate = _dateTime.Now,
            GeoSpeed = request.GeoSpeed
        };

        User user = await _userRepository.GetByIdAsync(request.UserId);
        if (user is null) throw new UserNotFoundException(nameof(request.UserId));

        user.Geolocalization = advancedGeolocalization;
        _userRepository.Update(user);
        await _unitOfWork.SaveChangesAsync();

        throw new NotImplementedException();
    }
}
