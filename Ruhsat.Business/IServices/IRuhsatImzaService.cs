using RuhsaProject.Entities.Concrete;
namespace RuhsaProject.Business.IServices
{
    public interface IRuhsatImzaService
    {
        Task<List<RuhsatImza>> GetAllAsync();
        Task<RuhsatImza> GetByIdAsync(int id);
        Task AddAsync(RuhsatImza entity);
        Task UpdateAsync(RuhsatImza entity);
        Task DeleteAsync(int id);
    }
}
