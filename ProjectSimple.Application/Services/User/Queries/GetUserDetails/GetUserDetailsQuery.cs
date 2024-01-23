using MediatR;

namespace ProjectSimple.Application.Services.User.Queries.GetUserDetails;

public record GetUserDetailsQuery(int Id) : IRequest<UserDetailsDTO>;
