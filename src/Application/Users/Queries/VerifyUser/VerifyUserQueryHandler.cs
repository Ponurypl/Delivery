using MultiProject.Delivery.Application.Common.Cryptography;
using MultiProject.Delivery.Application.Common.Failures;
using MultiProject.Delivery.Application.Common.Persistence.Repositories;

namespace MultiProject.Delivery.Application.Users.Queries.VerifyUser;

public sealed class VerifyUserQueryHandler : IQueryHandler<VerifyUserQuery, VerifiedUserDto>
{
    private readonly IHashService _hashService;
    private readonly IUserRepository _userRepository;

    public VerifyUserQueryHandler(IHashService hashService, IUserRepository userRepository)
    {
        _hashService = hashService;
        _userRepository = userRepository;
    }
    
    public async Task<ErrorOr<VerifiedUserDto>> Handle(VerifyUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);
        if (user is null)
        {
            return Failure.WrongUserOrPassword;
        }

        var passwordParts = user.Password.Split(":");
        var hash = _hashService.Hash(request.Password, passwordParts[1]);

        if (hash != passwordParts[0])
        {
            return Failure.WrongUserOrPassword;
        }

        return new VerifiedUserDto { Id = user.Id.Value, Username = user.Username };
    }
}