using NewsWebsite.Domain.Entities;

namespace NewsWebsite.Domain.Repositories.Interface
{
    public interface IUserRepository : IDisposable
    {
        IQueryable<User> GetQueryAble();
        Task<IEnumerable<User>> GetListAsync();
        Task<User?> GetByIdAsync(Guid guid);
        Task<bool> AnyAsync(Func<User, bool> func);
        IQueryable<User> Where(Func<User, bool> func);
        Task InsertAsync(User user);
        Task DeleteAsync(Guid guid);
        Task UpdateAsync(User user);
        Task SaveAsync();
    }
}
