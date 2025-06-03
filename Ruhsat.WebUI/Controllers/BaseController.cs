using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<User> _userManager;

        public BaseController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Kullanıcı oturum açmışsa tarayıcı önbelleğini devre dışı bırak
                context.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
                context.HttpContext.Response.Headers["Pragma"] = "no-cache";
                context.HttpContext.Response.Headers["Expires"] = "0"; // Geçmiş bir tarih veya 0

                var user = await _userManager.GetUserAsync(User);

                ViewBag.CurrentUserName = user?.UserName;
                ViewBag.CurrentUserPicture = string.IsNullOrEmpty(user?.Picture)
                    ? "/vuexy-bootstrap-html-admin-template/assets/img/avatars/default.png"
                    : user.Picture;

                // ✅ Rol bilgilerini ViewBag'e ekle
                var roles = await _userManager.GetRolesAsync(user);
                ViewBag.UserRoles = roles;
            }

            await next();
        }
    }
}
