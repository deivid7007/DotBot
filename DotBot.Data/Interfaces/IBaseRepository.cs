namespace DotBot.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task AddAsync(T entity);

        Task AddRangeAsync(List<T> entities);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
