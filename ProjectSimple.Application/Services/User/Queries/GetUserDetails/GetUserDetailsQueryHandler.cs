using AutoMapper;
using MediatR;
using ProjectSimple.Application.Exceptions;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Queries.GetUserDetails;

public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, UserDetailsDTO>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetUserDetailsQueryHandler> _appLogger;

    public GetUserDetailsQueryHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<GetUserDetailsQueryHandler> appLogger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _appLogger = appLogger;
    }

    public async Task<UserDetailsDTO> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
    {
        // Query database
        var user = await _userRepository.GetAsync(request.Id);

        // Verify object exists
        if (user == null)
        {
            throw new NotFoundException(nameof(User), request.Id);
        }

        // Convert object to dto object
        var result = _mapper.Map<UserDetailsDTO>(user);

        // Logging
        _appLogger.LogInformation("User retrieved successfully");

        // Return dto object
        return result;
    }
}