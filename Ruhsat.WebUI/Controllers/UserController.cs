using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.RoleDtos;
using RuhsaProject.DTOs.UserDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.DataAccess.Contexts;
using System.Security.Claims;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _environment;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly RuhsatDbContext _context;
        private readonly ILogService _logService;

        public UserController(
            UserManager<User> userManager,
            IWebHostEnvironment environment,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            RuhsatDbContext context,
            ILogService logService
            ) : base(userManager)
        {
            _userManager = userManager;
            _environment = environment;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _logService = logService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Success
            });
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (!ModelState.IsValid)
                return View("UserLogin", userLoginDto);

            var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View("UserLogin", userLoginDto);
            }

            if (await _userManager.IsLockedOutAsync(user))
            {
                ModelState.AddModelError("", "Hesabınız geçici olarak kilitlenmiştir. Lütfen daha sonra tekrar deneyin.");
                return View("UserLogin", userLoginDto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "E-Posta adresiniz veya şifreniz yanlıştır.");
                return View("UserLogin", userLoginDto);
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var permissions = new List<string>();

            foreach (var roleName in userRoles)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                if (role != null)
                {
                    var rolePermissions = await _context.RolePermissions
                        .Where(rp => rp.RoleId == role.Id)
                        .Include(rp => rp.Permission)
                        .Select(rp => rp.Permission.Name)
                        .ToListAsync();

                    permissions.AddRange(rolePermissions);
                }
            }

            permissions = permissions.Distinct().ToList();
            var claims = permissions.Select(p => new Claim("Permission", p)).ToList();

            await _signInManager.SignOutAsync();
            await _signInManager.SignInWithClaimsAsync(user, userLoginDto.RememberMe, claims);

            var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "Unknown";
            await _logService.AddLogAsync(user.Id.ToString(), user.UserName, "Login", "User", "Kullanıcı giriş yaptı.", ipAddress);

            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(UserAddDto userAddDto)
        {
            if (!ModelState.IsValid)
                return View(userAddDto);

            string uniqueFileName = null;

            if (userAddDto.Picture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + userAddDto.Picture.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await userAddDto.Picture.CopyToAsync(fileStream);
                }
            }

            var user = new User
            {
                UserName = userAddDto.UserName,
                Email = userAddDto.Email,
                PhoneNumber = userAddDto.PhoneNumber,
                Picture = "/img/" + uniqueFileName
            };

            var result = await _userManager.CreateAsync(user, userAddDto.Password);

            if (result.Succeeded)
            {
                await _logService.AddLogAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), User.Identity.Name ?? "Unknown", "Create User", "User", $"{user.UserName} kullanıcı oluşturuldu.");
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userAddDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Picture = user.Picture
            };

            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _logService.AddLogAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), User.Identity.Name ?? "Unknown", "Delete User", "User", $"{user.UserName} kullanıcı silindi.");
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Picture = user.Picture
            };

            return View("Delete", userDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return NotFound();

            var updateDto = new UserUpdateDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ExistingPicturePath = user.Picture
            };

            return View(updateDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDto userUpdateDto)
        {
            if (!ModelState.IsValid)
                return View(userUpdateDto);

            var user = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
            if (user == null)
                return NotFound();

            string uniqueFileName = user.Picture;

            if (userUpdateDto.Picture != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + userUpdateDto.Picture.FileName;
                string newPath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(newPath, FileMode.Create))
                {
                    await userUpdateDto.Picture.CopyToAsync(stream);
                }

                if (!string.IsNullOrEmpty(userUpdateDto.ExistingPicturePath))
                {
                    string oldPath = Path.Combine(_environment.WebRootPath, userUpdateDto.ExistingPicturePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }
            }

            user.UserName = userUpdateDto.UserName;
            user.Email = userUpdateDto.Email;
            user.PhoneNumber = userUpdateDto.PhoneNumber;
            user.Picture = "/img/" + Path.GetFileName(uniqueFileName);

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                await _logService.AddLogAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), User.Identity.Name ?? "Unknown", "Edit User", "User", $"{user.UserName} kullanıcı bilgileri güncellendi.");
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(userUpdateDto);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            await _logService.AddLogAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), User.Identity.Name ?? "Unknown", "Logout", "User", "Kullanıcı çıkış yaptı.");
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "User");

            var updateDto = new UserUpdateDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                ExistingPicturePath = user.Picture
            };

            return View("Edit", updateDto);
        }

        [Authorize]
        [HttpGet]
        public ViewResult PasswordChange()
        {
            return View("PasswordChange");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var isVerified = await _userManager.CheckPasswordAsync(user, userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _userManager.ChangePasswordAsync(user, userPasswordChangeDto.CurrentPassword,
                        userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword, true, false);
                        await _logService.AddLogAsync(user.Id.ToString(), user.UserName, "Password Change", "User", "Kullanıcı şifresini değiştirdi.");
                        TempData.Add("SuccessMessage", $"Şifreniz başarıyla değiştirilmiştir.");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Lütfen, girmiş olduğunuz şu anki şifrenizi kontrol ediniz.");
                    return View(userPasswordChangeDto);
                }
            }
            else
            {
                return View(userPasswordChangeDto);
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Assign(int userId)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound();

            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await _userManager.GetRolesAsync(user);

            var roleAssignDto = new UserRoleAssignDto
            {
                UserId = user.Id,
                UserName = user.UserName
            };

            foreach (var role in roles)
            {
                roleAssignDto.RoleAssignDtos.Add(new RoleAssignDto
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    HasRole = userRoles.Contains(role.Name)
                });
            }

            return PartialView("_RoleAssignPartial", roleAssignDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(UserRoleAssignDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
                return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var roleDto in model.RoleAssignDtos)
            {
                if (roleDto.HasRole && !userRoles.Contains(roleDto.RoleName))
                {
                    await _userManager.AddToRoleAsync(user, roleDto.RoleName);
                }
                else if (!roleDto.HasRole && userRoles.Contains(roleDto.RoleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, roleDto.RoleName);
                }
            }

            await _logService.AddLogAsync(User.FindFirstValue(ClaimTypes.NameIdentifier), User.Identity.Name ?? "Unknown", "Role Assign", "User", $"{user.UserName} kullanıcısına roller atandı/güncellendi.");

            TempData["SuccessMessage"] = $"{user.UserName} kullanıcısının rol bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
