using RuhsaProject.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuhsaProject.Core.Interfaces
{
    public interface IRuhsatImzaRepository
    {
        Task<List<RuhsatImza>> GetAllAsync();
        Task<RuhsatImza> GetByIdAsync(int id);
        Task AddAsync(RuhsatImza entity);
        Task UpdateAsync(RuhsatImza entity);
        Task DeleteAsync(int id);
    }
}
