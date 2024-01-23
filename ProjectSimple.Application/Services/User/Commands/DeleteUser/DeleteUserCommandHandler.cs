using AutoMapper;
using MediatR;
using ProjectSimple.Application.Exceptions;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Commands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<DeleteUserCommandHandler> _appLogger;

    public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<DeleteUserCommandHandler> appLogger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        // Retrieve domain entity object
        var userToDelete = await _userRepository.GetAsync(request.Id);

        // Verify record exists
        if (userToDelete == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        // Delete from database
        await _userRepository.DeleteAsync(userToDelete);

        // Logging
        _appLogger.LogInformation("User deleted successfully");

        // Return record Id
        return Unit.Value;
    }
}
