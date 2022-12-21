using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;
using MultiProject.Delivery.Domain.Users.Entities;
using MultiProject.Delivery.Domain.Users.ValueTypes;

namespace MultiProject.Delivery.Application.Users.Queries.GetUserLocation;
public sealed class GetUserLocationQueryHandler : IQueryHandler<GetUserLocationQuery, GetUserLocationDto>
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public GetUserLocationQueryHandler(IMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<GetUserLocationDto>> Handle(GetUserLocationQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(new UserId(request.UserId), cancellationToken);
        if (user is null)
        {
            return Failure.UserNotExists;
        }

        return _mapper.Map<GetUserLocationDto>(user);
    }
}
