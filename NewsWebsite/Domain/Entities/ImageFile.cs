using NewsWebsite.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class ImageFile : EntityBase
    {
        [Required]
        public string Url { get; set; } = string.Empty;
        [Required]
        public virtual NewsPost Post { get; set; } = null!;

        public string Description { get; set; } = string.Empty;
    }
}
