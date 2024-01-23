using MediatR;

namespace ProjectSimple.Application.Services.User.Queries.GetUsers;

public record GetUsersQuery(bool? isActive = null) : IRequest<List<UserDTO>>;