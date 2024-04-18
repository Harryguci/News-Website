namespace Domain.Domain.Repositories.Interface
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetQueryAble();
        Task<IEnumerable<T>> GetListAsync();
        Task<T?> GetByIdAsync(Guid guid);
        Task<bool> AnyAsync(Func<T, bool> func);
        IQueryable<T> Where(Func<T, bool> func);
        Task InsertAsync(T item);
        Task DeleteAsync(Guid guid);
        Task UpdateAsync(T item);
        Task SaveAsync();
    }
}
