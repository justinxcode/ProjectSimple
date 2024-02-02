using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Services.User.Commands.GetUsers;
using ProjectSimple.Domain.Models;
using ProjectSimple.Infrastructure.DatabaseContext;
using Telerik.DataSource;
using Telerik.DataSource.Extensions;

namespace ProjectSimple.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DefaultContext dbcontext, IMapper mapper) : base(dbcontext)
    {
    }

    public async Task<bool> IsUsernameUnique(string username, long? Id = null)
    {
        return await _dbContext.Users.AnyAsync(x => x.Username == username && (Id == null || x.Id != Id)) == false;
    }

    public async Task<DataSourceResult> GetAllAsync(GetUsersCommand getUsersCommand)
    {
        return await _dbContext.Set<User>()
            .AsNoTracking()
            .ToDataSourceResultAsync(getUsersCommand);
    }
}