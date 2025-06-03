using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.RuhsatSinifiDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.Business.IServices;
using System.Security.Claims;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor,User")]
    public class RuhsatSinifiController : BaseController
    {
        private readonly IRuhsatSinifiService _ruhsatSinifiService;
        private readonly IRuhsatTuruService _ruhsatTuruService;
        private readonly ILogService _logService;

        public RuhsatSinifiController(
            IRuhsatSinifiService ruhsatSinifiService,
            IRuhsatTuruService ruhsatTuruService,
            ILogService logService,
            UserManager<User> userManager) : base(userManager)
        {
            _ruhsatSinifiService = ruhsatSinifiService;
            _ruhsatTuruService = ruhsatTuruService;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _ruhsatSinifiService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadRuhsatTurleriAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RuhsatSinifiDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadRuhsatTurleriAsync();
                return View(dto);
            }

            await _ruhsatSinifiService.AddAsync(dto);
            await LogAsync("Create", $"Yeni Ruhsat Sınıfı eklendi: {dto.Name}");

            TempData["SuccessMessage"] = "Ruhsat sınıfı başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _ruhsatSinifiService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            await LoadRuhsatTurleriAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RuhsatSinifiDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await LoadRuhsatTurleriAsync();
                return View(dto);
            }

            var old = await _ruhsatSinifiService.GetByIdAsync(id);
            await _ruhsatSinifiService.UpdateAsync(dto);

            await LogAsync("Update", $"Ruhsat Sınıfı güncellendi: '{old?.Name}' → '{dto.Name}'");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _ruhsatSinifiService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dto = await _ruhsatSinifiService.GetByIdAsync(id);
            var name = dto?.Name ?? "Unknown";

            await _ruhsatSinifiService.DeleteAsync(id);
            await LogAsync("Delete", $"Ruhsat Sınıfı silindi: {name}");

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadRuhsatTurleriAsync()
        {
            var turler = await _ruhsatTuruService.GetAllAsync();
            ViewBag.RuhsatTurleri = new SelectList(turler, "Id", "Name");
        }

        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "RuhsatSinifi", description, ip);
        }
    }
}
