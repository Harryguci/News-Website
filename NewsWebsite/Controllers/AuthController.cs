using Domain.Domain.Common;
using Microsoft.AspNetCore.Mvc;
using NewsWebsite.Domain.Dtos;
using NewsWebsite.Domain.Repositories.Interface;
using Domain.Services.Interfaces;
using NewsWebsite.Domain.Entities;
using Domain.Middleware;
using System.IdentityModel.Tokens.Jwt;

namespace Domain.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private IUserRepository _userRepository;
    private IUserService _userService;

    private static readonly string MySecret = "asdv234234^&%&^%&^hjsdfb2%%%";

    public AuthController(IUserRepository userRepository, IUserService userService)
    {
        _userRepository = userRepository;
        _userService = userService;
    }

    [HttpPost("Login")]
    public IActionResult Login([Bind("username,password")] AuthenticateRequest account)
    {
        var response = _userService.Authenticate(account);

        if (response == null)
            return BadRequest(new { message = "Your username or password is incorrect" });

        var currentUser = _userRepository
            .Where(p => p.Username == account.Username)
            .FirstOrDefault();

        if (currentUser == null)
        {
            return BadRequest();
        }
        return Ok(response);
    }

    [HttpPost("Signup")]
    public async Task<IActionResult> SignUpAsync([Bind("username,password,roleString,phone")] UserDto account)
    {
        // AC$000000
        if (await _userRepository.AnyAsync(p => p.Username == account.Username))
        {
            return BadRequest(new
            {
                error = "username is already exist",
            });
        }

        var hashPass = SecurePasswordHasher.Hash(account.Password);

        account.Password = hashPass;

        await _userRepository.InsertAsync(new User()
        {
            Guid = Guid.NewGuid(),
            Username = account.Username,
            Password = account.Password,
            Role = account.Role != null ? account.Role.ToString() : "user",
            Phone = account.Phone,
        });

        try
        {
            await _userRepository.SaveAsync();
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                error = ex.Message
            });
        }

        return Ok(account);
    }

    [HttpGet("Logout")]
    [Authorize]
    public IActionResult Logout()
    {
        var currentUser = HttpContext.Items["User"] as UserDto;
        if (currentUser == null)
        {
            return BadRequest();
        }

        return Ok(currentUser);
    }

    [NonAction]
    public async Task<bool> IsAccountExist(Guid id)
    {
        return await _userRepository.AnyAsync(p => p.Guid == id);
    }

    [NonAction]
    public static string GetClaim(string token, string claimType)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (securityToken == null) return "";

        var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
        return stringClaimValue;
    }

}
