using MediatR;

namespace ProjectSimple.Application.Services.User.Queries.GetUserDetails;

public record GetUserDetailsQuery(long Id) : IRequest<UserDetailsDTO>;
