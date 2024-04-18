using NewsWebsite.Domain.Entities;

namespace Domain.Domain.Repositories.Interface
{
    public interface INewsPostRepository : IDisposable
    {
        IQueryable<NewsPost> GetQueryAble();
        Task<IEnumerable<NewsPost>> GetListAsync();
        Task<NewsPost?> GetByIdAsync(Guid guid);
        Task<bool> AnyAsync(Func<NewsPost, bool> func);
        IQueryable<NewsPost> Where(Func<NewsPost, bool> func);
        Task InsertAsync(NewsPost newsPost);
        Task DeleteAsync(Guid guid);
        Task UpdateAsync(NewsPost newsPost);
        Task SaveAsync();
    }
}
