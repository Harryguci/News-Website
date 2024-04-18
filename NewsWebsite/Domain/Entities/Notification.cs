using NewsWebsite.Domain.Entities;

namespace Domain.Domain.Entities
{
    public class Notification : EntityBase
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}
