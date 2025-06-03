using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RuhsaProject.Business.IServices;
using RuhsaProject.DTOs.RuhsatImzaDtos;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using System.Security.Claims;

namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "Admin,Editor,User")]
    public class RuhsatImzaController : BaseController
    {
        private readonly IRuhsatImzaService _ruhsatImzaService;
        private readonly ILogService _logService;
        private readonly IMapper _mapper;

        public RuhsatImzaController(
            IRuhsatImzaService ruhsatImzaService,
            ILogService logService,
            UserManager<User> userManager,
            IMapper mapper) : base(userManager)
        {
            _ruhsatImzaService = ruhsatImzaService;
            _logService = logService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var entityList = await _ruhsatImzaService.GetAllAsync();
            var dtoList = _mapper.Map<List<RuhsatImzaDto>>(entityList);
            return View(dtoList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RuhsatImzaDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var entity = _mapper.Map<RuhsatImza>(dto);
            await _ruhsatImzaService.AddAsync(entity);
            await LogAsync("Create", $"Yeni Ruhsat İmza eklendi: {dto.AdSoyad}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await _ruhsatImzaService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<RuhsatImzaDto>(entity);
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RuhsatImzaDto dto)
        {
            if (id != dto.Id) return NotFound();
            if (!ModelState.IsValid) return View(dto);

            var entity = _mapper.Map<RuhsatImza>(dto);
            await _ruhsatImzaService.UpdateAsync(entity);
            await LogAsync("Update", $"Ruhsat İmza güncellendi: {dto.AdSoyad}");

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var entity = await _ruhsatImzaService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            var dto = _mapper.Map<RuhsatImzaDto>(entity);
            return View(dto);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var entity = await _ruhsatImzaService.GetByIdAsync(id);
            if (entity == null) return NotFound();

            await _ruhsatImzaService.DeleteAsync(id);
            await LogAsync("Delete", $"Ruhsat İmza silindi: {entity.AdSoyad}");

            return RedirectToAction(nameof(Index));
        }

        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "RuhsatImza", description, ip);
        }
    }
}
