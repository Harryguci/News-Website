using NewsWebsite.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Entities
{
    public class PersonalNotification : Notification
    {
        [Required]
        public virtual User User { get; set; } = null!;
        public PersonalNotification(User user)
        {
            User = user;
        }
    }
}
