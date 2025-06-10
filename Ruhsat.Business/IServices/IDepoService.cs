
using RuhsaProject.DTOs.DepoDtos;

namespace RuhsaProject.Business.IServices
{
    public interface IDepoService
    {
        Task<List<DepoDto>> GetAllAsync();
        Task<DepoDto> GetByIdAsync(int id);
        Task AddAsync(DepoDto dto);
        Task UpdateAsync(DepoDto dto);
        Task DeleteAsync(int id);
    }
}
