using Domain.Domain.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsPostsController : ControllerBase
    {
        private INewsPostRepository _newsPostRepository;
        public NewsPostsController(INewsPostRepository newsPostRepository)
        {
            _newsPostRepository = newsPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var list = await _newsPostRepository.GetListAsync();

            return Ok(list);
        }
    }
}
