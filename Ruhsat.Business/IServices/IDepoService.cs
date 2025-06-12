
using RuhsaProject.DTOs.DepoDtos;
using RuhsaProject.Entities.Concrete;
using System.Linq.Expressions;

namespace RuhsaProject.Business.IServices
{
    public interface IDepoService
    {
        Task<List<DepoDto>> GetAllAsync();

        // Filtreli hali → predicate ile
        Task<List<DepoDto>> GetAllAsync(Expression<Func<Depo, bool>> predicate);

        Task<DepoDto> GetByIdAsync(int id);
        Task AddAsync(DepoDto dto);
        Task UpdateAsync(DepoDto dto);
        Task DeleteAsync(int id);

        // Depo entity olarak döndürür
        Task<List<Depo>> GetListByRuhsatSinifiIdAsync(int ruhsatSinifiId);

    }
}
