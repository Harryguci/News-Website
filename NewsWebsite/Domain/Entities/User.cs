using Domain.Domain.Entities;
using NewsWebsite.Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NewsWebsite.Domain.Entities
{
    public class User : EntityBase, IUser
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public int Rewards { get; set; } = 0;
        public virtual List<NewsPost> Posts { get; set; } = [];
        public virtual List<Notification> Notifications { get; set; } = [];
    }
}
