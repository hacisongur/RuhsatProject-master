using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.DepoDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.Business.IServices;
using System.Security.Claims;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor,User")]
    public class DepoController : BaseController
    {
        private readonly IDepoService _depoService;
        private readonly IRuhsatSinifiService _ruhsatSinifiService;
        private readonly ILogService _logService;

        public DepoController(
            IDepoService depoService,
            IRuhsatSinifiService ruhsatSinifiService,
            ILogService logService,
            UserManager<User> userManager) : base(userManager)
        {
            _depoService = depoService;
            _ruhsatSinifiService = ruhsatSinifiService;
            _logService = logService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _depoService.GetAllAsync();
            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadRuhsatSiniflariAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepoDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadRuhsatSiniflariAsync();
                return View(dto);
            }

            await _depoService.AddAsync(dto);
            await LogAsync("Create", $"Yeni Depo eklendi: {dto.Adi}");

            TempData["SuccessMessage"] = "Depo başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _depoService.GetByIdAsync(id);
            if (dto == null) return NotFound();

            await LoadRuhsatSiniflariAsync();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepoDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                await LoadRuhsatSiniflariAsync();
                return View(dto);
            }

            var old = await _depoService.GetByIdAsync(id);
            await _depoService.UpdateAsync(dto);

            await LogAsync("Update", $"Depo güncellendi: '{old?.Adi}' → '{dto.Adi}'");
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var dto = await _depoService.GetByIdAsync(id);
            if (dto == null) return NotFound();
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dto = await _depoService.GetByIdAsync(id);
            var name = dto?.Adi ?? "Unknown";

            await _depoService.DeleteAsync(id);
            await LogAsync("Delete", $"Depo silindi: {name}");

            return RedirectToAction(nameof(Index));
        }

        private async Task LoadRuhsatSiniflariAsync()
        {
            var siniflar = await _ruhsatSinifiService.GetAllAsync();
            ViewBag.RuhsatSiniflari = new SelectList(siniflar, "Id", "Name");
        }

        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "Depo", description, ip);
        }
        [HttpGet]
        public async Task<IActionResult> GetDepolarByRuhsatSinifiId(int ruhsatSinifiId)
        {
            var depolar = await _depoService.GetListByRuhsatSinifiIdAsync(ruhsatSinifiId);

            // DEBUG → Şunu buraya koy ve kontrol et:
            Console.WriteLine($"DepoController → GetDepolarByRuhsatSinifiId çağrıldı → RuhsatSinifiId: {ruhsatSinifiId}, Depo Sayısı: {depolar.Count}");

            var result = depolar.Select(x => new
            {
                id = x.Id,
                adi = x.Adi
            }).ToList();

            return Json(result);
        }

    }
}
