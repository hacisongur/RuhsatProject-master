using RuhsaProject.Entities.Concrete;
using RuhsatProject.DTOs.Ruhsat;
using System.Linq.Expressions;

namespace RuhsatProject.Business.IServices
{
    public interface IRuhsatService
    {
        Task<List<RuhsatDto>> GetAllAsync();
        Task<RuhsatDto> GetByIdAsync(int id);
        Task AddAsync(RuhsatDto dto);
        Task UpdateAsync(RuhsatDto dto);
        Task DeleteAsync(int id);

    }
}
