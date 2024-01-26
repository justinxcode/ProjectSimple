using AutoMapper;
using MediatR;
using ProjectSimple.Application.Exceptions;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Services.User.Queries.GetUsers;
using ProjectSimple.Application.Validations;

namespace ProjectSimple.Application.Services.User.Commands.GetUsers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, GetUsersCommandResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IAppLogger<GetUsersCommandHandler> _logger;

    public GetUsersCommandHandler(IUserRepository userRepository, IMapper mapper, IAppLogger<GetUsersCommandHandler> appLogger)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _logger = appLogger;
    }

    public async Task<GetUsersCommandResponse> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        // Validate incoming data
        var validator = new PaginationValidator(typeof(UserDTO));
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count != 0)
        {
            throw new BadRequestException("Invalid Pagination", validationResult);
        }

        // Query database
        var (userList, count) = await _userRepository.GetAllAsync(request);

        // Convert objects to dto objects
        var userDtoList = _mapper.Map<List<UserDTO>>(userList);

        var result = _mapper.Map<GetUsersCommandResponse>(userDtoList);

        result.Page = request.Page;
        result.PageSize = request.PageSize;
        result.TotalCount = count;

        // Logging
        _logger.LogInformation("Paginated users retrieved successfully");

        // Return list of dto objects
        return result;
    }
}
