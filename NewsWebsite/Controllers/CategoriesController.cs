using Domain.Domain.Entities;
using Domain.Domain.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryRepository _categoryRepository;
        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync()
        {
            var list = await _categoryRepository.GetListAsync();
            return Ok(list);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOne([Bind("name,display")]Category category)
        {
            category.Guid = Guid.NewGuid();
            await _categoryRepository.InsertAsync(category);
            await _categoryRepository.SaveAsync();

            return NoContent();
        }
    }
}
