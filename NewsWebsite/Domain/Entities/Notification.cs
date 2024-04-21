using NewsWebsite.Domain.Entities;

namespace Domain.Domain.Entities
{
    public class Notification : EntityBase
    {
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool Deleted { get; set; } = false;
    }
}
