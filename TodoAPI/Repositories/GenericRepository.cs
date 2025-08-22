using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Utilities;
using System.Linq.Expressions;
using TodoAPI.Data;
using TodoAPI.Models;

namespace TodoAPI.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<(IEnumerable<T>Items, int TotalCount)> GetAllAsync(int userId, int pageNumber, int pageSize)
        {
            if (typeof(T) == typeof(TodoItem))
            {
                var query = _dbSet.Cast<TodoItem>().Where(t => t.UserId == userId);               
                var totalCount = await query.CountAsync();               
                var items = await query
                    .OrderByDescending(t => t.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync();

                return (items.Cast<T>(), totalCount);
            }            
            var allItems = await _dbSet.ToListAsync();
            return (allItems, allItems.Count);
        }

        public async Task<T> GetByIdAsync(int id, int userId)
        {
            if (typeof(T) == typeof(TodoItem))
            {
                var entity = await _dbSet.Cast<TodoItem>()
                                         .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
                return (T)(object)entity;
            }
            return await _dbSet.FindAsync(id);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
