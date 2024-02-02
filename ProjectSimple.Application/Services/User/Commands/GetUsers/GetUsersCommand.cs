using MediatR;
using ProjectSimple.Application.Models;
using ProjectSimple.Application.Services.User.Queries.GetUsers;
using Telerik.DataSource;

namespace ProjectSimple.Application.Services.User.Commands.GetUsers;

public class GetUsersCommand() : DataSourceRequest, IRequest<DataEnvelope<UserDTO>>;