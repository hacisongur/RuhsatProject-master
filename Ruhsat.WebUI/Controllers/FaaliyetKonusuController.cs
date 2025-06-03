using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.FaaliyetKonusuDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.Business.IServices;
using System.Security.Claims;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor,User")]
    public class FaaliyetKonusuController : BaseController
    {
        private readonly IFaaliyetKonusuService _faaliyetKonusuService;
        private readonly ILogService _logService;

        // ❗️Tek constructor, DI ile uyumlu şekilde
        public FaaliyetKonusuController(
            IFaaliyetKonusuService faaliyetKonusuService,
            ILogService logService,
            UserManager<User> userManager) : base(userManager)
        {
            _faaliyetKonusuService = faaliyetKonusuService;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var faaliyetKonulari = await _faaliyetKonusuService.GetAllAsync();
            return View(faaliyetKonulari);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FaaliyetKonusuDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _faaliyetKonusuService.AddAsync(dto);

            await LogAsync("Create", $"Yeni Faaliyet Konusu eklendi: {dto.Name}");
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _faaliyetKonusuService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FaaliyetKonusuDto dto)
        {
            if (id != dto.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(dto);

            // 🔍 Önce eski değeri çekiyoruz
            var existingDto = await _faaliyetKonusuService.GetByIdAsync(id);
            if (existingDto == null)
                return NotFound();

            await _faaliyetKonusuService.UpdateAsync(dto);

            // 📝 Önceki ve güncel değeri logla
            string message = $"Faaliyet Konusu güncellendi: '{existingDto.Name}' → '{dto.Name}'";
            await LogAsync("Update", message);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _faaliyetKonusuService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dto = await _faaliyetKonusuService.GetByIdAsync(id);
            var name = dto?.Name ?? "Unknown";

            await _faaliyetKonusuService.DeleteAsync(id);

            await LogAsync("Delete", $"Faaliyet Konusu silindi: {name}");
            return RedirectToAction(nameof(Index));
        }

        // 🔒 Ortak log fonksiyonu
        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "FaaliyetKonusu", description, ip);
        }
    }
}
