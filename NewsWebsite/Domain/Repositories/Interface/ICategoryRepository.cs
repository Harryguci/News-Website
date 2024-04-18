using Domain.Domain.Entities;

namespace Domain.Domain.Repositories.Interface
{
    public interface ICategoryRepository : IRepositoryBase<Category>, IDisposable
    {
        Task<Category?> FindByName(string name);
    }
}
