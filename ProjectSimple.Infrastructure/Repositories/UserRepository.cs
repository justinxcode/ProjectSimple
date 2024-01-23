using Microsoft.EntityFrameworkCore;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Domain.Models;
using ProjectSimple.Infrastructure.DatabaseContext;

namespace ProjectSimple.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DefaultContext dbcontext) : base(dbcontext)
    {
    }

    public async Task<bool> IsUsernameUnique(string username)
    {
        return await _dbContext.Users.AnyAsync(x => x.Username == username) == false;
    }
}
