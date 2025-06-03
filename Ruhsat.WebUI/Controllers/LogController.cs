using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.DataAccess.Contexts;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin")] // Sadece Admin görebilir
    public class LogController : BaseController
    {
        private readonly RuhsatDbContext _context;

        public LogController(RuhsatDbContext context, UserManager<User> userManager): base(userManager) // BaseController'ın yapıcı metodunu çağırıyoruz
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var logs = await _context.LogEntries.OrderByDescending(l => l.Timestamp).ToListAsync();
            return View(logs);
        }
    }
}
