using ProjectSimple.Domain.Models;

namespace ProjectSimple.Application.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<bool> IsUsernameUnique(string username, long? Id = null);
    Task<IReadOnlyList<User>> GetAllAsync(bool isActive);
}