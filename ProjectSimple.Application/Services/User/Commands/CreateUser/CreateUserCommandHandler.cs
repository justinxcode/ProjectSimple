using AutoMapper;
using MediatR;
using ProjectSimple.Application.Exceptions;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, long>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<CreateUserCommandHandler> _appLogger;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<CreateUserCommandHandler> appLogger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _appLogger = appLogger;
    }

    public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new CreateUserCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            throw new BadRequestException("Invalid User", validationResult);
        }

        // Convert to domain entity object
        var userToCreate = _mapper.Map<Domain.Models.User>(request);

        // Add to database
        await _userRepository.CreateAsync(userToCreate);

        // Logging
        _appLogger.LogInformation("User created successfully");

        // Return Id
        return userToCreate.Id;
    }
}
