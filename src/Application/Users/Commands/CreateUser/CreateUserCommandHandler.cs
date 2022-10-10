using MultiProject.Delivery.Application.Common.Interfaces.Repositories;
using MultiProject.Delivery.Domain.Users.Entities;

namespace MultiProject.Delivery.Application.Users.Commands.CreateUser;

public sealed class CreateUserCommandHandler : IHandler<CreateUserCommand, UserCreatedDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserCreatedDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User newUser = new()
        {
            Id = Guid.NewGuid(),
            IsActive = true,
            Login = request.Login,
            Password = request.Password,
            PhoneNumber = request.PhoneNumber,
            Role = request.Role
        };
                
        _userRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync();

        return new UserCreatedDto { Id = newUser.Id};
    }
}
