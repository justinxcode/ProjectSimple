using AutoMapper;
using MediatR;
using ProjectSimple.Application.Interfaces;

namespace ProjectSimple.Application.Services.User.Queries.GetUsers;

public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserDTO>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetUsersQueryHandler> _logger;

    public GetUsersQueryHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<GetUsersQueryHandler> appLogger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = appLogger;
    }

    public async Task<List<UserDTO>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        // Query database
        var users = await _userRepository.GetAllAsync();

        // Convert objects to dto objects
        var result = _mapper.Map<List<UserDTO>>(users);

        // Logging
        _logger.LogInformation("Users retrieved successfully");

        // Return list of dto objects
        return result;
    }
}
