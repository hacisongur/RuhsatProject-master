
using RuhsaProject.DTOs.DashboardCardDtos;
using RuhsatProject.DTOs.Ruhsat;

namespace RuhsatProject.Business.IServices
{
    public interface IRuhsatService
    {
        Task<List<RuhsatDto>> GetAllAsync();
        Task<RuhsatDto> GetByIdAsync(int id);
        Task AddAsync(RuhsatDto dto);
        Task UpdateAsync(RuhsatDto dto);
        Task DeleteAsync(int id);
        Task<List<RuhsatDto>> SearchAsync(string term);
        Task<IList<RuhsatDto>> GetByActiveStatusAsync(bool isActive);
        Task<List<DashboardCardDto>> GetDashboardCardsAsync();


    }

}
