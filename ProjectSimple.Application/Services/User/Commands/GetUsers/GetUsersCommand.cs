using MediatR;
using ProjectSimple.Application.Models;

namespace ProjectSimple.Application.Services.User.Commands.GetUsers;

public class GetUsersCommand() : Pagination, IRequest<GetUsersCommandResponse>;