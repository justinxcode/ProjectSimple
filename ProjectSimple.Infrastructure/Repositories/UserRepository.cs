using Microsoft.EntityFrameworkCore;
using ProjectSimple.Application.Interfaces;
using ProjectSimple.Application.Models;
using ProjectSimple.Domain.Models;
using ProjectSimple.Infrastructure.DatabaseContext;
using static ProjectSimple.Application.Helpers.PaginationBuilder;

namespace ProjectSimple.Infrastructure.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(DefaultContext dbcontext) : base(dbcontext)
    {
    }

    public async Task<(IReadOnlyList<User> data, int count)> GetAllAsync(Pagination pagination)
    {
        // Declare paging variables
        int page = pagination.Page;
        int pageSize = pagination.PageSize;

        // Build filtering expression
        var filters = BuildFilters<User>(pagination.Filters);

        // Build base query
        var query = _dbContext.Set<User>().AsNoTracking().AsQueryable();

        // Apply filtering to query
        var filteredQuery = query.Where(filters);

        // Get count after filtering
        // Seems to be more efficient to make two db calls instead of grouping
        var count = await filteredQuery.CountAsync();

        // Apply sorting to query
        var sortedQuery = ApplySorting(filteredQuery, pagination.Sorting);

        // Get data from db
        var data = await sortedQuery
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

        return (data, count);

    }

    public async Task<bool> IsUsernameUnique(string username, long? Id = null)
    {
        return await _dbContext.Users.AnyAsync(x => x.Username == username && (Id == null || x.Id != Id)) == false;
    }
}