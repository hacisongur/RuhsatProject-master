using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
public class AuthorizePermissionAttribute : AuthorizeAttribute, IAsyncAuthorizationFilter
{
    private readonly string _permission;

    public AuthorizePermissionAttribute(string permission)
    {
        _permission = permission;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var user = context.HttpContext.User;

        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // Eğer kullanıcı Admin rolündeyse izin kontrolünü atla
        if (user.IsInRole("Admin"))
        {
            await Task.CompletedTask;
            return;
        }

        if (!user.Claims.Any(c => c.Type == "Permission" && c.Value == _permission))
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
