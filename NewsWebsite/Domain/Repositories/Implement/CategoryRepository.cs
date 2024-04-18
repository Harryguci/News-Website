using Domain.Domain.Entities;
using Domain.Domain.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using NewsWebsite.Migrations;

namespace Domain.Domain.Repositories.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private NewsDbContext context;
        public CategoryRepository(NewsDbContext context)
        {
            this.context = context;
        }

        public async Task<bool> AnyAsync(Func<Category, bool> func) => await context.Categories.AnyAsync(p => func(p));
        public async Task DeleteAsync(Guid guid)
        {
            var category = await context.Categories.FindAsync(guid);
            if (category != null)
            {
                context.Categories.Remove(category);
            }
        }

        public async Task<Category?> GetByIdAsync(Guid guid) => await context.Categories.FindAsync(guid);
        public async Task<IEnumerable<Category>> GetListAsync() => await context.Categories.ToListAsync();
        public IQueryable<Category> GetQueryAble() => context.Categories;

        public async Task InsertAsync(Category item)
        {
            await context.Categories.AddAsync(item);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(Category item)
        {
            context.Entry(item).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public IQueryable<Category> Where(Func<Category, bool> func)
        {
            return context.Categories.Where(x => func(x));
        }

        public async Task<Category?> FindByName(string name)
        {
            return await context.Categories.FirstOrDefaultAsync(x => x.Name == name);
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
