using NewsWebsite.Domain.Dtos;

namespace Domain.Domain.Common;
public class AuthenticateResponse
{
    public string Id { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string? Phone { get; set; }
    public string Role { get; set; } = null!;
    public string Token { get; set; }
    public AuthenticateResponse(UserDto user, string token)
    {
        Username = user.Username;
        Phone = user.Phone;
        Role = user.RoleString ?? "";
        Token = token;
    }
}
