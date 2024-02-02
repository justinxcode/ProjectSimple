using AutoMapper;
using MediatR;
using ProjectSimple.Application.Exceptions;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Commands.UpdateUser;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<UpdateUserCommandHandler> _appLogger;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<UpdateUserCommandHandler> appLogger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _appLogger = appLogger;
    }

    public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        // Retrieve domain entity object
        var userToUpdate = await _userRepository.GetAsync(request.Id);

        // Verify record exists
        if (userToUpdate == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        // Validate incoming data
        var validator = new UpdateUserCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid User", validationResult);
        }

        // Update record with existing values
        _mapper.Map(request, userToUpdate);

        // Update database
        await _userRepository.UpdateAsync(userToUpdate);

        // Logging
        _appLogger.LogInformation("User updated successfully");

        return Unit.Value;
    }
}