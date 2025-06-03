using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.RoleDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.DataAccess.Contexts;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly RuhsatDbContext _context;
        private readonly ILogService _logService;

        public RoleController(RoleManager<Role> roleManager, UserManager<User> userManager, RuhsatDbContext context, ILogService logService) : base(userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(new RoleListDto
            {
                Roles = roles,
                ResultStatus = ResultStatus.Success
            });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoleAddDto roleAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new Role
                {
                    Name = roleAddDto.Name,
                    NormalizedName = roleAddDto.Name.ToUpper()
                });

                if (result.Succeeded)
                {
                    await LogAsync("Create", $"Rol oluşturuldu: {roleAddDto.Name}");
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }

            return View(roleAddDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            var roleDto = new RoleDto
            {
                Id = role.Id,
                Name = role.Name
            };

            return View(roleDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                await LogAsync("Delete", $"Rol silindi: {role.Name}");
                return RedirectToAction(nameof(Index));
            }

            ModelState.AddModelError("", "Silme işlemi başarısız oldu.");
            return View("Delete", new RoleDto { Id = role.Id, Name = role.Name });
        }

        [HttpGet]
        public async Task<IActionResult> AssignPermissions(int roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId.ToString());
            if (role == null)
                return NotFound();

            var allPermissions = await _context.Permissions.ToListAsync();
            var rolePermissions = await _context.RolePermissions
                                        .Where(rp => rp.RoleId == role.Id)
                                        .Select(rp => rp.PermissionId)
                                        .ToListAsync();

            var model = new RolePermissionAssignDto
            {
                RoleId = role.Id,
                RoleName = role.Name
            };

            foreach (var permission in allPermissions)
            {
                model.PermissionAssignDtos.Add(new PermissionAssignDto
                {
                    PermissionId = permission.Id,
                    PermissionName = permission.Name,
                    HasPermission = rolePermissions.Contains(permission.Id)
                });
            }

            return PartialView("_AssignPermissionsPartial", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignPermissions(RolePermissionAssignDto model)
        {
            var role = await _roleManager.FindByIdAsync(model.RoleId.ToString());
            if (role == null)
                return NotFound();

            var currentRolePermissions = await _context.RolePermissions
                                             .Where(rp => rp.RoleId == role.Id)
                                             .ToListAsync();

            foreach (var permissionDto in model.PermissionAssignDtos)
            {
                var hasPermission = currentRolePermissions.Any(rp => rp.PermissionId == permissionDto.PermissionId);

                if (permissionDto.HasPermission && !hasPermission)
                {
                    _context.RolePermissions.Add(new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permissionDto.PermissionId
                    });
                }
                else if (!permissionDto.HasPermission && hasPermission)
                {
                    var toRemove = currentRolePermissions.First(rp => rp.PermissionId == permissionDto.PermissionId);
                    _context.RolePermissions.Remove(toRemove);
                }
            }

            await _context.SaveChangesAsync();

            await LogAsync("Assign Permissions", $"{role.Name} rolüne ait izinler güncellendi.");
            TempData["SuccessMessage"] = $"{role.Name} rolüne ait izinler güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "Role", description, ip);
        }
    }
}
