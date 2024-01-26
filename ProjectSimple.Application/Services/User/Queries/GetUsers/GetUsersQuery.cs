using MediatR;

namespace ProjectSimple.Application.Services.User.Queries.GetUsers;

public record GetUsersQuery() : IRequest<List<UserDTO>>;