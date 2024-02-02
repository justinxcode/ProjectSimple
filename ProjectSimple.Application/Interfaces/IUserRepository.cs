using ProjectSimple.Application.Services.User.Commands.GetUsers;
using ProjectSimple.Domain.Models;
using Telerik.DataSource;

namespace ProjectSimple.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> IsUsernameUnique(string username, long? Id = null);

    Task<DataSourceResult> GetAllAsync(GetUsersCommand getUsersCommand);
}