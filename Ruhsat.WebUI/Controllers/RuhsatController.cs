using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using RuhsaProject.Business.IServices;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.WebUI.Controllers;
using RuhsatProject.Business.IServices;
using RuhsatProject.DTOs.Ruhsat;
using System.Globalization;
using System.Security.Claims;

using System.Globalization;
namespace RuhsatProject.WebUI.Controllers
{
    [Authorize(Roles = "User,Editor,Admin")]
    public class RuhsatController : BaseController
    {
        private readonly IRuhsatService _ruhsatService;
        private readonly IFaaliyetKonusuService _faaliyetKonusuService;
        private readonly IRuhsatTuruService _ruhsatTuruService;
        private readonly IRuhsatSinifiService _ruhsatSinifiService;
        private readonly ILogService _logService;

        public RuhsatController(
            IRuhsatService ruhsatService,

            IFaaliyetKonusuService faaliyetKonusuService,
            IRuhsatTuruService ruhsatTuruService,
            IRuhsatSinifiService ruhsatSinifiService,
            UserManager<User> userManager, ILogService logService) : base(userManager)
        {
            _ruhsatService = ruhsatService;
            _faaliyetKonusuService = faaliyetKonusuService;
            _ruhsatTuruService = ruhsatTuruService;
            _ruhsatSinifiService = ruhsatSinifiService;
            _logService = logService;
        }
        public async Task<IActionResult> Index()
        {
            // 1. Dashboard kartları verisini çek
            var dashboardCards = await _ruhsatService.GetDashboardCardsAsync();

            // 2. ViewBag ile View'a gönder
            ViewBag.DashboardCards = dashboardCards;

            // 3. Ruhsat listesini (boş liste olarak) gönder, çünkü ilk yüklemede filtreleme yapılmayacak
            var emptyList = new List<RuhsatDto>();
            return View(emptyList);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Json(new List<RuhsatDto>());

            var filtered = await _ruhsatService.SearchAsync(term);
            return Json(filtered);
        }

        // GET: Ruhsat/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var dto = new RuhsatDto
            {
                IsActive = true
            };

            var faaliyetKonulari = await _faaliyetKonusuService.GetAllAsync();
            ViewBag.FaaliyetKonulari = new SelectList(faaliyetKonulari, "Id", "Name");

            var ruhsatTurleri = await _ruhsatTuruService.GetAllAsync();
            ViewBag.RuhsatTurleri = new SelectList(ruhsatTurleri, "Id", "Name");

            var ruhsatSiniflari = await _ruhsatSinifiService.GetAllAsync();
            ViewBag.RuhsatSiniflari = new SelectList(ruhsatSiniflari, "Id", "Name");

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RuhsatDto dto, IFormFile photoFile, IFormFile? scannedFile)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var faaliyetKonulari = await _faaliyetKonusuService.GetAllAsync();
                    ViewBag.FaaliyetKonulari = new SelectList(faaliyetKonulari, "Id", "Name");

                    var ruhsatTurleri = await _ruhsatTuruService.GetAllAsync();
                    ViewBag.RuhsatTurleri = new SelectList(ruhsatTurleri, "Id", "Name");

                    var ruhsatSiniflari = await _ruhsatSinifiService.GetAllAsync();
                    ViewBag.RuhsatSiniflari = new SelectList(ruhsatSiniflari, "Id", "Name");

                    return View(dto);
                }

                // Fotoğraf dosyası kaydı
                if (photoFile != null && photoFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    var uniquePhotoName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                    var photoPath = Path.Combine(uploadsFolder, uniquePhotoName);

                    using var stream = new FileStream(photoPath, FileMode.Create);
                    await photoFile.CopyToAsync(stream);

                    dto.PhotoPath = "/uploads/" + uniquePhotoName;
                }

                // Taranmış belge kaydı - sadece dosya yüklendiğinde işlem yap
                if (scannedFile != null && scannedFile.Length > 0)
                {
                    var scanFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "scans");
                    if (!Directory.Exists(scanFolder)) Directory.CreateDirectory(scanFolder);

                    var uniqueScanName = Guid.NewGuid().ToString() + Path.GetExtension(scannedFile.FileName);
                    var scanPath = Path.Combine(scanFolder, uniqueScanName);

                    using var stream = new FileStream(scanPath, FileMode.Create);
                    await scannedFile.CopyToAsync(stream);

                    dto.ScannedFilePath = "/uploads/scans/" + uniqueScanName;
                }

                // Veritabanına kaydetme işlemi
                await _ruhsatService.AddAsync(dto);
                await LogAsync("Create", $"Yeni ruhsat eklendi: {dto.Adi} {dto.Soyadi} - Ruhsat No: {dto.RuhsatNo}");
                TempData["SuccessMessage"] = "Ruhsat başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // Hata detaylarını loglama
                var errorMessage = new System.Text.StringBuilder();
                errorMessage.AppendLine($"Ana Hata: {ex.Message}");

                // Exception hierarşisini logla
                if (ex.InnerException != null)
                {
                    errorMessage.AppendLine($"İç Hata: {ex.InnerException.Message}");

                    if (ex.InnerException.InnerException != null)
                    {
                        errorMessage.AppendLine($"İç İç Hata: {ex.InnerException.InnerException.Message}");
                    }
                }

                // Veritabanı işlemleriyle ilgili daha fazla bilgi ekleyin
                errorMessage.AppendLine($"DTO Data: {System.Text.Json.JsonSerializer.Serialize(dto)}");  // Eklenen veri

                // Hata mesajını TempData'ya ekleyin
                TempData["ErrorMessage"] = errorMessage.ToString();

                // Dropdown listelerini tekrar doldur
                var faaliyetKonulari = await _faaliyetKonusuService.GetAllAsync();
                ViewBag.FaaliyetKonulari = new SelectList(faaliyetKonulari, "Id", "Name");

                var ruhsatTurleri = await _ruhsatTuruService.GetAllAsync();
                ViewBag.RuhsatTurleri = new SelectList(ruhsatTurleri, "Id", "Name");

                var ruhsatSiniflari = await _ruhsatSinifiService.GetAllAsync();
                ViewBag.RuhsatSiniflari = new SelectList(ruhsatSiniflari, "Id", "Name");

                return View(dto);
            }
        }


        // GET: Ruhsat/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ruhsat = await _ruhsatService.GetByIdAsync(id);
            if (ruhsat == null) return NotFound();

            // Edit view'ı için de dropdownları doldur
            var faaliyetKonulari = await _faaliyetKonusuService.GetAllAsync();
            ViewBag.FaaliyetKonulari = new SelectList(faaliyetKonulari, "Id", "Name", ruhsat.FaaliyetKonusuId);

            var ruhsatTurleri = await _ruhsatTuruService.GetAllAsync();
            ViewBag.RuhsatTurleri = new SelectList(ruhsatTurleri, "Id", "Name", ruhsat.RuhsatTuruId);

            var ruhsatSiniflari = await _ruhsatSinifiService.GetAllAsync();
            ViewBag.RuhsatSiniflari = new SelectList(ruhsatSiniflari, "Id", "Name", ruhsat.RuhsatSinifiId);

            return View(ruhsat);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(RuhsatDto dto, IFormFile? photoFile)
        {
            if (ModelState.IsValid)
            {
                // Eski ruhsatı çek
                var existing = await _ruhsatService.GetByIdAsync(dto.Id);
                if (existing == null)
                    return NotFound();

                // Eğer yeni fotoğraf gelmemişse, eski fotoğrafı tut
                if (photoFile == null || photoFile.Length == 0)
                {
                    dto.PhotoPath = existing.PhotoPath;
                }
                else
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await photoFile.CopyToAsync(stream);

                    dto.PhotoPath = "/uploads/" + uniqueFileName;

                    // Eğer eski fotoğraf varsa sil
                    if (!string.IsNullOrEmpty(existing.PhotoPath) && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existing.PhotoPath.TrimStart('/'))))
                    {
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existing.PhotoPath.TrimStart('/')));
                    }
                }

                await _ruhsatService.UpdateAsync(dto);
                await LogAsync("Update", $"Ruhsat güncellendi: ID {dto.Id} - {dto.Adi} {dto.Soyadi}");
                TempData["SuccessMessage"] = "Ruhsat başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }

            // Model valid değilse dropdownları tekrar doldur
            var faaliyetKonulari = await _faaliyetKonusuService.GetAllAsync();
            ViewBag.FaaliyetKonulari = new SelectList(faaliyetKonulari, "Id", "Name", dto.FaaliyetKonusuId);

            var ruhsatTurleri = await _ruhsatTuruService.GetAllAsync();
            ViewBag.RuhsatTurleri = new SelectList(ruhsatTurleri, "Id", "Name", dto.RuhsatTuruId);

            var ruhsatSiniflari = await _ruhsatSinifiService.GetAllAsync();
            ViewBag.RuhsatSiniflari = new SelectList(ruhsatSiniflari, "Id", "Name", dto.RuhsatSinifiId);

            return View(dto);
        }
        [HttpPost]
        public async Task<IActionResult> UploadScannedFile(int id, IFormFile scannedFile)
        {
            if (scannedFile == null || scannedFile.Length == 0)
            {
                TempData["ErrorMessage"] = "Dosya seçilmedi.";
                return RedirectToAction(nameof(Index));
            }

            var ruhsat = await _ruhsatService.GetByIdAsync(id);
            if (ruhsat == null)
            {
                TempData["ErrorMessage"] = "Ruhsat bulunamadı.";
                return RedirectToAction(nameof(Index));
            }

            // Dosya kaydı
            var scanFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "scans");
            if (!Directory.Exists(scanFolder)) Directory.CreateDirectory(scanFolder);

            var uniqueScanName = Guid.NewGuid().ToString() + Path.GetExtension(scannedFile.FileName);
            var scanPath = Path.Combine(scanFolder, uniqueScanName);

            using var stream = new FileStream(scanPath, FileMode.Create);
            await scannedFile.CopyToAsync(stream);

            ruhsat.ScannedFilePath = "/uploads/scans/" + uniqueScanName;

            await _ruhsatService.UpdateAsync(ruhsat);
            await LogAsync("UploadScan", $"Taranmış dosya yüklendi: Ruhsat ID {ruhsat.Id}, Dosya: {ruhsat.ScannedFilePath}");
            TempData["SuccessMessage"] = "Taranmış dosya başarıyla yüklendi.";
            return RedirectToAction(nameof(Index));
        }


        // GET: Ruhsat/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ruhsat = await _ruhsatService.GetByIdAsync(id);
            if (ruhsat == null) return NotFound();
            return View(ruhsat);
        }

        // POST: Ruhsat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Ruhsat silinmeden önce fotoğrafı sil
            var ruhsat = await _ruhsatService.GetByIdAsync(id);
            if (ruhsat != null && !string.IsNullOrEmpty(ruhsat.PhotoPath) && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ruhsat.PhotoPath.TrimStart('/'))))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ruhsat.PhotoPath.TrimStart('/')));
            }

            await _ruhsatService.DeleteAsync(id);
            await LogAsync("Delete", $"Ruhsat silindi: ID {ruhsat?.Id} - {ruhsat?.Adi} {ruhsat?.Soyadi}");
            TempData["SuccessMessage"] = "Ruhsat başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GenerateReport(int id)
        {
            var ruhsat = await _ruhsatService.GetByIdAsync(id);
            if (ruhsat == null) return NotFound();
            await LogAsync("GenerateReport", $"PDF raporu oluşturuldu: Ruhsat ID {ruhsat.Id}, Ruhsat No: {ruhsat.RuhsatNo}");
            return new ViewAsPdf("_RuhsatPdf", ruhsat)
            {
                FileName = $"Ruhsat_{ruhsat.RuhsatNo}.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageMargins = new Rotativa.AspNetCore.Options.Margins(2,2,2,2),
                CustomSwitches = "--disable-smart-shrinking"
            };
        }

        private async Task LogAsync(string action, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.Identity?.Name ?? "Unknown";
            var ip = HttpContext.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";

            await _logService.AddLogAsync(userId, userName, action, "Ruhsat", description, ip);
        }
        [HttpGet]
        public async Task<IActionResult> GetByStatus(string status)
        {
            if (string.IsNullOrWhiteSpace(status) || status == "all")
            {
                var all = await _ruhsatService.GetAllAsync();
                return Json(all);
            }

            bool isActive = bool.Parse(status);
            var filtered = await _ruhsatService.GetByActiveStatusAsync(isActive);
            return Json(filtered);
        }
 


    }
}
