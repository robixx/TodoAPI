using Org.BouncyCastle.Utilities;
using System.Linq.Expressions;

namespace TodoAPI.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<(IEnumerable<T>Items, int TotalCount)> GetAllAsync(int userId, int pageNumber, int pageSize);
        Task<T> GetByIdAsync(int id, int userId);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
