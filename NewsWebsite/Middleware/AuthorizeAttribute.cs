using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NewsWebsite.Domain.Entities;
using NewsWebsite.Domain.Dtos;
using NewsWebsite.Domain.ValueObjects;

namespace Domain.Middleware;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private string? _roles;

    public AuthorizeAttribute()
    {
        // Accept all roles to access!!
    }


    public AuthorizeAttribute(string? roles)
    {
        if (!roles.IsNullOrEmpty()) _roles = roles;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.Items["User"] as UserDto;
        if (user == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
        }

        if (_roles != null && user?.Role != new RoleObject(_roles.ToLower()))
        {
            // have no right to do
            context.Result = new JsonResult(new { message = "You can not access this" }) { StatusCode = StatusCodes.Status406NotAcceptable };
        }
    }
}
