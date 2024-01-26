using ProjectSimple.Application.Services.User.Queries.GetUsers;

namespace ProjectSimple.Application.Services.User.Commands.GetUsers;

public class GetUsersCommandResponse
{
    public required List<UserDTO> Users { get; set; }

    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    public bool HasPrevPage => Page > 1;
    public bool HasNextPage => Page * PageSize < TotalCount;
}