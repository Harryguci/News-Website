using Domain.Domain.Entities;
using NewsWebsite.Domain.Entities;
using NewsWebsite.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Domain.Dtos
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? RoleString
        {
            get
            {
                if (Role != null)
                    return Role.ToString();
                else return "user";
            }
            set { Role = new RoleObject(value ?? "user"); }
        }
        public RoleObject? Role { get; set; } = new RoleObject(RoleEnum.USER);
        public string? Phone { get; set; }
        public int? Rewards { get; set; } = 0;

        public UserDto() { }
        public UserDto(string username, string password, string? phone = null, string? role = null)
        {
            Username = username;
            Password = password;

            if (phone != null)
            {
                Phone = phone;
            }
            if (role != null)
            {
                Role = new RoleObject(role.ToLower());
                RoleString = role.ToLower();
            }
        }
    }
}
