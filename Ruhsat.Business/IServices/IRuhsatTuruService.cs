using RuhsaProject.DTOs.RuhsatTuruDtos;

namespace RuhsatProject.Business.IServices
{
    public interface IRuhsatTuruService
    {
        Task<List<RuhsatTuruDto>> GetAllAsync();
        Task<RuhsatTuruDto> GetByIdAsync(int id);
        Task AddAsync(RuhsatTuruDto dto);
        Task UpdateAsync(RuhsatTuruDto dto);
        Task DeleteAsync(int id);
    }
}
