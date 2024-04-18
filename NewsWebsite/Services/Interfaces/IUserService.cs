using Domain.Domain.Common;
using NewsWebsite.Domain.Dtos;

namespace Domain.Services.Interfaces
{
    public interface IUserService
    {
        AuthenticateResponse? Authenticate(AuthenticateRequest model);
        Task<IEnumerable<UserDto>> GetAllAsync();
        UserDto? GetById(Guid id);
    }
}
