using Domain.Domain.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Domain.Entities;

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

        [HttpPost]
        public async Task<IActionResult> CreateOne([Bind("guid,title,description,content,category,images")] NewsPost post)
        {
            post.Guid = Guid.NewGuid();
            try
            {
                await _newsPostRepository.InsertAsync(post);
                await _newsPostRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return NoContent();
        }
    }
}
