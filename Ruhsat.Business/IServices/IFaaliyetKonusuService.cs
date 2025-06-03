using RuhsaProject.DTOs.FaaliyetKonusuDtos;

namespace RuhsatProject.Business.IServices
{
    public interface IFaaliyetKonusuService
    {
        Task<List<FaaliyetKonusuDto>> GetAllAsync();
        Task<FaaliyetKonusuDto> GetByIdAsync(int id);
        Task AddAsync(FaaliyetKonusuDto dto);
        Task UpdateAsync(FaaliyetKonusuDto dto);
        Task DeleteAsync(int id);
    }
}
