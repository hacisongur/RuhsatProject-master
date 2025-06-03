
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Core.Interfaces
{
    public interface IFaaliyetKonusuRepository
    {
        Task<List<FaaliyetKonusu>> GetAllAsync(); // Tüm FaaliyetKonusu'ları alır
        Task<FaaliyetKonusu> GetByIdAsync(int id); // ID ile FaaliyetKonusu alır
        Task AddAsync(FaaliyetKonusu faaliyetKonusu); // Yeni FaaliyetKonusu ekler
        Task UpdateAsync(FaaliyetKonusu faaliyetKonusu); // Mevcut FaaliyetKonusu'yu günceller
        Task DeleteAsync(int id); // FaaliyetKonusu'yu siler
    }
}
