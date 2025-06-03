using Microsoft.EntityFrameworkCore;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.EntityFramework.Repositories
{
    public class RuhsatSinifiRepository : IRuhsatSinifiRepository
    {
        private readonly RuhsatDbContext _context;

        public RuhsatSinifiRepository(RuhsatDbContext context)
        {
            _context = context;
        }

        public async Task<List<RuhsatSinifi>> GetAllAsync()
        {
            return await _context.RuhsatSiniflari
                .Include(rs => rs.RuhsatTuru)
                .ToListAsync();
        }

        public async Task<RuhsatSinifi> GetByIdAsync(int id)
        {
            return await _context.RuhsatSiniflari.FindAsync(id);
        }

        public async Task AddAsync(RuhsatSinifi ruhsatSinifi)
        {
            try
            {
                Console.WriteLine($"RuhsatSinifiRepository.AddAsync - Entity: Name={ruhsatSinifi.Name}, RuhsatTuruId={ruhsatSinifi.RuhsatTuruId}");

                await _context.RuhsatSiniflari.AddAsync(ruhsatSinifi);
                Console.WriteLine("Entity context'e eklendi");

                var result = await _context.SaveChangesAsync();
                Console.WriteLine($"SaveChangesAsync sonucu: {result} satır etkilendi");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RuhsatSinifiRepository.AddAsync - Hata: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task UpdateAsync(RuhsatSinifi updatedEntity)
        {
            var existingEntity = await _context.RuhsatSiniflari.FindAsync(updatedEntity.Id);
            if (existingEntity == null)
                return;

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.RuhsatSiniflari.FindAsync(id);
            if (entity != null)
            {
                try
                {
                    _context.RuhsatSiniflari.Remove(entity);
                    var affectedRows = await _context.SaveChangesAsync();

                    // Etkilenen satır sayısını kontrol et
                    if (affectedRows == 0)
                    {
                        // Loglama veya özel bir hata fırlatma yapılabilir
                        Console.WriteLine($"Concurrency hatası: {id} ID'li Ruhsat Sınıfı silinirken 0 satır etkilendi.");
                        // İsteğe bağlı olarak kullanıcıya bilgi vermek için bir istisna fırlatılabilir
                        // throw new DbUpdateConcurrencyException("Silmeye çalıştığınız kayıt başkası tarafından değiştirilmiş veya silinmiş.");
                    }
                    else
                    {
                        Console.WriteLine($"{id} ID'li Ruhsat Sınıfı başarıyla silindi. Etkilenen satır sayısı: {affectedRows}");
                    }
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    // Concurrency hatasını yakala ve logla
                    Console.WriteLine($"DbUpdateConcurrencyException yakalandı: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    // Hatayı yeniden fırlat veya farklı şekilde ele al
                    throw;
                }
                catch (Exception ex)
                {
                    // Diğer hataları yakala ve logla
                    Console.WriteLine($"DeleteAsync sırasında beklenmeyen hata: {ex.Message}");
                    Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                    throw;
                }
            }
            else
            {
                Console.WriteLine($"{id} ID'li Ruhsat Sınıfı zaten mevcut değil veya bulunamadı.");
            }
        }
    }
}
