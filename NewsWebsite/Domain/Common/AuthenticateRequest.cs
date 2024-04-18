using System.ComponentModel.DataAnnotations;

namespace Domain.Domain.Common;

public class AuthenticateRequest
{
    [Required]
    public string Username { get; set; } = "";

    [Required]
    public string Password { get; set; } = "";
}
