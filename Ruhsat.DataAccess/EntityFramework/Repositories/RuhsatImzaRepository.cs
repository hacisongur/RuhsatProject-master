using Microsoft.EntityFrameworkCore;
using RuhsaProject.Entities.Concrete;
using RuhsaProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;

namespace RuhsaProject.DataAccess.EntityFramework.Repositories
{
    public class RuhsatImzaRepository : IRuhsatImzaRepository
    {
        private readonly RuhsatDbContext _context;

        public RuhsatImzaRepository(RuhsatDbContext context)
        {
            _context = context;
        }

        public async Task<List<RuhsatImza>> GetAllAsync()
        {
            return await _context.RuhsatImzalar.ToListAsync();
        }

        public async Task<RuhsatImza> GetByIdAsync(int id)
        {
            return await _context.RuhsatImzalar.FindAsync(id);
        }

        public async Task AddAsync(RuhsatImza entity)
        {
            await _context.RuhsatImzalar.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RuhsatImza entity)
        {
            _context.RuhsatImzalar.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.RuhsatImzalar.FindAsync(id);
            if (entity != null)
            {
                _context.RuhsatImzalar.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
