
using RuhsaProject.Entities.Concrete;
using System.Linq.Expressions;

namespace RuhsaProject.Core.Interfaces
{
    public interface IDepoRepository
    {
        Task<List<Depo>> GetAllAsync(); // Tüm Depo'ları alır
        Task<Depo> GetByIdAsync(int id); // ID ile Depo alır
        Task AddAsync(Depo depo); // Yeni Depo ekler
        Task UpdateAsync(Depo depo); // Mevcut Depo'yu günceller
        Task DeleteAsync(int id); // Depo'yu siler
        Task<List<Depo>> GetAllAsync(Expression<Func<Depo, bool>> predicate); // BUNU EKLE

    }
}
