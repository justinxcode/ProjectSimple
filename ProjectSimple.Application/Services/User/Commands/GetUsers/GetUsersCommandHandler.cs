using AutoMapper;
using MediatR;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Models;
using ProjectSimple.Application.Services.User.Queries.GetUsers;
using Telerik.DataSource;

namespace ProjectSimple.Application.Services.User.Commands.GetUsers;

public class GetUsersCommandHandler : IRequestHandler<GetUsersCommand, DataEnvelope<UserDTO>>
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

    public async Task<DataEnvelope<UserDTO>> Handle(GetUsersCommand request, CancellationToken cancellationToken)
    {
        // Query database
        var processedData = await _userRepository.GetAllAsync(request);

        DataEnvelope<UserDTO> result;

        // Convert objects to dto objects
        if (request.Groups.Count > 0)
        {
            var groupedData = processedData.Data
                                .Cast<AggregateFunctionsGroup>()
                                .Select(MapGroup).ToList();

            // If there is grouping, use the field for grouped data
            // The app must be able to serialize and deserialize it
            // Example helper methods for this are available in this project
            // See the GroupDataHelper.DeserializeGroups and JsonExtensions.Deserialize methods
            result = new DataEnvelope<UserDTO>
            {
                GroupedData = groupedData,
                CurrentPageData = null,
                TotalItemCount = processedData.Total
            };
        }
        else
        {
            // Map the flat list of Users to UserDTOs
            var currentPageData = processedData.Data
                                    .Cast<Domain.Models.User>()
                                    .Select(user => _mapper.Map<UserDTO>(user))
                                    .ToList();

            // When there is no grouping, the simplistic approach of 
            // just serializing and deserializing the flat data is enough
            result = new DataEnvelope<UserDTO>
            {
                GroupedData = null,
                CurrentPageData = currentPageData,
                TotalItemCount = processedData.Total
            };
        }

        // Logging
        _logger.LogInformation("Paginated users retrieved successfully");

        // Return list of dto objects
        return result;
    }
    private AggregateFunctionsGroup MapGroup(AggregateFunctionsGroup group)
    {
        return new AggregateFunctionsGroup
        {
            Key = group.Key,
            Items = group.HasSubgroups ?
                    group.Items.Cast<AggregateFunctionsGroup>().Select(MapGroup).ToList() :
                    group.Items.Cast<Domain.Models.User>().Select(item => _mapper.Map<UserDTO>(item)).ToList(),
            HasSubgroups = group.HasSubgroups,
            Member = group.Member,
            ItemCount = group.ItemCount,
            AggregateFunctionsProjection = group.AggregateFunctionsProjection,
        };
    }
}