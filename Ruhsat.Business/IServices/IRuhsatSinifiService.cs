using RuhsaProject.DTOs.RuhsatSinifiDtos;

namespace RuhsatProject.Business.IServices
{
    public interface IRuhsatSinifiService
    {
        Task<List<RuhsatSinifiDto>> GetAllAsync();
        Task<RuhsatSinifiDto> GetByIdAsync(int id);
        Task AddAsync(RuhsatSinifiDto dto);
        Task UpdateAsync(RuhsatSinifiDto dto);
        Task DeleteAsync(int id);
    }
}
