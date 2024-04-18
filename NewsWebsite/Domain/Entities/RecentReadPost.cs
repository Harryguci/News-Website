using NewsWebsite.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class RecentReadPost : EntityBase
    {
        [Required]
        public virtual User User { get; set; } = null!;
        [Required]
        public virtual NewsPost Post { get; set; } = null!;
    }
}
