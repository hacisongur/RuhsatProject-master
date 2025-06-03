using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.RuhsatTuruDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.Business.IServices;
using System.Security.Claims;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor,User")]
    public class RuhsatTuruController : BaseController
    {
        private readonly IRuhsatTuruService _ruhsatTuruService;
        private readonly ILogService _logService;

        public RuhsatTuruController(
            IRuhsatTuruService ruhsatTuruService,
            ILogService logService,
            UserManager<User> userManager) : base(userManager)
        {
            _ruhsatTuruService = ruhsatTuruService;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _ruhsatTuruService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RuhsatTuruDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _ruhsatTuruService.AddAsync(dto);
            await LogAsync("Create", $"Yeni Ruhsat Türü eklendi: {dto.Name}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _ruhsatTuruService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RuhsatTuruDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            var old = await _ruhsatTuruService.GetByIdAsync(id);
            await _ruhsatTuruService.UpdateAsync(dto);

            await LogAsync("Update", $"Ruhsat Türü güncellendi: '{old?.Name}' → '{dto.Name}'");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _ruhsatTuruService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dto = await _ruhsatTuruService.GetByIdAsync(id);
            var name = dto?.Name ?? "Unknown";

            await _ruhsatTuruService.DeleteAsync(id);
            await LogAsync("Delete", $"Ruhsat Türü silindi: {name}");

            return RedirectToAction(nameof(Index));
        }

        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "RuhsatTuru", description, ip);
        }
    }
}
