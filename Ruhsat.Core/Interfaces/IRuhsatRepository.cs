using RuhsaProject.Entities.Concrete;
using RuhsatProject.Entities.Concrete;
using System.Linq.Expressions;

namespace RuhsatProject.Core.Interfaces
{
    public interface IRuhsatRepository
    {
        Task<List<Ruhsat>> GetAllAsync();
        Task<Ruhsat> GetByIdAsync(int id);
        Task AddAsync(Ruhsat ruhsat);
        Task UpdateAsync(Ruhsat ruhsat);
        Task DeleteAsync(int id);
        Task<List<Ruhsat>> SearchAsync(string term);
        Task<List<Ruhsat>> GetAllAsync(Expression<Func<Ruhsat, bool>> predicate);

    }
}
