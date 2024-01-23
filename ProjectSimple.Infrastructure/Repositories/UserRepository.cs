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

    public async Task<bool> IsUsernameUnique(string username, long? Id = null)
    {
        if (Id == null)
        {
            // Check for new user creation
            return await _dbContext.Users.AnyAsync(x => x.Username == username) == false;
        }
        else
        {
            // Check for user update - exclude the current user from the check
            return await _dbContext.Users.AnyAsync(x => x.Username == username && x.Id != Id) == false;
        }
    }

    public async Task<IReadOnlyList<User>> GetAllAsync(bool isActive)
    {
        return await _dbContext.Set<User>()
            .Where(x => x.IsActive == isActive)
            .AsNoTracking()
            .ToListAsync();
    }
}
