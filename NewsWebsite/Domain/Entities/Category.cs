using NewsWebsite.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class Category
    {
        [Key]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Display { get; set; } = string.Empty;

        public virtual IEnumerable<NewsPost> NewsPosts { get; set; } = [];
    }
}
