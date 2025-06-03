using Microsoft.EntityFrameworkCore;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.EntityFramework.Repositories
{
    public class RuhsatTuruRepository : IRuhsatTuruRepository
    {
        private readonly RuhsatDbContext _context;

        public RuhsatTuruRepository(RuhsatDbContext context)
        {
            _context = context;
        }

        public async Task<List<RuhsatTuru>> GetAllAsync()
        {
            return await _context.RuhsatTurleri.ToListAsync();
        }

        public async Task<RuhsatTuru> GetByIdAsync(int id)
        {
            return await _context.RuhsatTurleri.FindAsync(id);
        }

        public async Task AddAsync(RuhsatTuru ruhsatTuru)
        {
            await _context.RuhsatTurleri.AddAsync(ruhsatTuru);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RuhsatTuru updatedEntity)
        {
            var existingEntity = await _context.RuhsatTurleri.FindAsync(updatedEntity.Id);
            if (existingEntity == null)
                return;

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.RuhsatTurleri.FindAsync(id);
            if (entity != null)
            {
                _context.RuhsatTurleri.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
