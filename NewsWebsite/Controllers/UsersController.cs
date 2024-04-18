using Domain.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Domain.Entities;
using NewsWebsite.Domain.Repositories.Interface;

namespace NewsWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<User>> GetList()
        {
            return await _userRepository.GetListAsync();
        }

        [HttpPost]
        public User CreateOne(User user)
        {
            _userRepository.InsertAsync(user);
            _userRepository.SaveAsync();
            return user;
        }
    }
}
