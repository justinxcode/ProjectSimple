using ProjectSimple.Application.Models;
using ProjectSimple.Domain.Models;

namespace ProjectSimple.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<(IReadOnlyList<User> data, int count)> GetAllAsync(Pagination pagination);
    Task<bool> IsUsernameUnique(string username, long? Id = null);
}