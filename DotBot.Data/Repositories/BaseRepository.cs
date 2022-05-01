using DotBot.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotBot.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly SqlDbContext _context;

        public BaseRepository(SqlDbContext context)
        {
            _context = context;
        }

        public async virtual Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);

            await this._context.SaveChangesAsync();
        }

        public async virtual Task AddRangeAsync(List<T> entities)
        {
            _context.Set<T>().AddRange(entities);

            await this._context.SaveChangesAsync();
        }

        public async virtual Task UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            await this._context.SaveChangesAsync();
        }

        public async virtual Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await this._context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }
    }
}
