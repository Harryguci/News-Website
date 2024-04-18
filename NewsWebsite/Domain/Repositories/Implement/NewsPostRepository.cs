using Domain.Domain.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Domain.Entities;
using NewsWebsite.Migrations;

namespace Domain.Domain.Repositories.Implement
{
    public class NewsPostRepository : INewsPostRepository
    {
        private NewsDbContext context;
        public NewsPostRepository(NewsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AnyAsync(Func<NewsPost, bool> func)
        {
            return await context.NewsPosts.AnyAsync(x => func(x));
        }

        public async Task DeleteAsync(Guid guid)
        {
            NewsPost? user = await context.NewsPosts.FindAsync(guid);
            if (user == null)
            {
                return;
            }
            context.NewsPosts.Remove(user);
        }

        public async Task<NewsPost?> GetByIdAsync(Guid guid)
        {
            return await context.NewsPosts.FindAsync(guid);
        }

        public async Task<IEnumerable<NewsPost>> GetListAsync()
        {
            return await context.NewsPosts.ToListAsync();
        }

        public IQueryable<NewsPost> GetQueryAble()
        {
            return context.NewsPosts;
        }

        public async Task InsertAsync(NewsPost newsPost)
        {
            await context.NewsPosts.AddAsync(newsPost);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(NewsPost newsPost)
        {
            context.Entry(newsPost).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public IQueryable<NewsPost> Where(Func<NewsPost, bool> func)
        {
            return context.NewsPosts.Where(news => func(news));
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
