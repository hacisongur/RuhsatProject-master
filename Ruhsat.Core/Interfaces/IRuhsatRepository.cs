using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Core.Interfaces
{
    public interface IRuhsatRepository
    {
        Task<List<Ruhsat>> GetAllAsync();
        Task<Ruhsat> GetByIdAsync(int id);
        Task AddAsync(Ruhsat ruhsat);
        Task UpdateAsync(Ruhsat ruhsat);
        Task DeleteAsync(int id);
    }
}
