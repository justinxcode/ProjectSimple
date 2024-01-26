using ProjectSimple.Application.Models;
using ProjectSimple.Domain.Models.Common;

namespace ProjectSimple.Application.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task<T> DeleteAsync(T entity);
    Task<T> GetAsync(long Id);
    Task<IReadOnlyList<T>> GetAllAsync();
}