
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Core.Interfaces
{
    public interface IRuhsatTuruRepository
    {
        Task<List<RuhsatTuru>> GetAllAsync(); // Tüm RuhsatTuru'ları alır
        Task<RuhsatTuru> GetByIdAsync(int id); // ID ile RuhsatTuru alır
        Task AddAsync(RuhsatTuru ruhsatTuru); // Yeni RuhsatTuru ekler
        Task UpdateAsync(RuhsatTuru ruhsatTuru); // Mevcut RuhsatTuru'yu günceller
        Task DeleteAsync(int id); // RuhsatTuru'yu siler
    }
}
