using Microsoft.AspNetCore.Mvc.RazorPages;
using NewsWebsite.Domain.Entities;
using Newtonsoft.Json;

namespace Web.Pages.Users
{
    public class IndexModel : PageModel
    {
        public IEnumerable<User> Users { get; set; } = [];
        public ILogger<IndexModel> _logger { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task OnGet()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync($"https://localhost:7024/api/Users");
                var json = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    Users = JsonConvert.DeserializeObject<IEnumerable<User>>(json) ?? [];
                }
                else
                {
                    _logger.LogInformation(json);
                    HttpContext.Response.Redirect("/login");
                }
            }
        }
    }
}
