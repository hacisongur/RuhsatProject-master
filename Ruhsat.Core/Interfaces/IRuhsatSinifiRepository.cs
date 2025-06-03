
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Core.Interfaces
{
    public interface IRuhsatSinifiRepository
    {
        Task<List<RuhsatSinifi>> GetAllAsync(); // Tüm RuhsatSinifi'leri alır
        Task<RuhsatSinifi> GetByIdAsync(int id); // ID ile RuhsatSinifi alır
        Task AddAsync(RuhsatSinifi ruhsatSinifi); // Yeni RuhsatSinifi ekler
        Task UpdateAsync(RuhsatSinifi ruhsatSinifi); // Mevcut RuhsatSinifi'yi günceller
        Task DeleteAsync(int id); // RuhsatSinifi'yi siler
    }
}
