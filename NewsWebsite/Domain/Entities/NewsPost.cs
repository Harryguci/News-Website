using Domain.Domain.Entities;

namespace NewsWebsite.Domain.Entities
{
    public class NewsPost : EntityBase
    {
        public string? Title { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public DateTime? CreateAt { get; set; }
        public string? Status { get; set; } = string.Empty;
        public bool Accepted { get; set; } = false;
        public virtual List<ImageFile>? Images { get; set; } = [];
        public virtual List<Category>? Category { get; set; } = [];
    }
}
