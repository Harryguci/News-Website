using Microsoft.EntityFrameworkCore;
using NewsWebsite.Domain.Entities;
using NewsWebsite.Domain.Repositories.Interface;
using NewsWebsite.Migrations;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Domain.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private NewsDbContext context;
        public UserRepository(NewsDbContext context)
        {
            this.context = context;
        }

        public IQueryable<User> GetQueryAble()
        {
            return context.Users;
        }

        public async Task<bool> AnyAsync(Func<User, bool> func)
        {
            return await context.Users.AnyAsync(x => func(x));
        }

        public async Task DeleteAsync(Guid guid)
        {
            User? user = await context.Users.FindAsync(guid);
            if (user == null)
            {
                return;
            }
            context.Users.Remove(user);
        }

        public async Task<User?> GetByIdAsync(Guid guid)
        {
            return await context.Users.FindAsync(guid);
        }

        public async Task<IEnumerable<User>> GetListAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task InsertAsync(User user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public Task UpdateAsync(User user)
        {
            context.Entry(user).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public IQueryable<User> Where(Func<User, bool> func)
        {
            return context.Users.Where(func).AsQueryable();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
