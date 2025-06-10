using Microsoft.EntityFrameworkCore;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;
using RuhsatProject.Entities.Concrete;
using System.Linq;
using System.Linq.Expressions;

namespace RuhsatProject.DataAccess.EntityFramework.Repositories
{
    public class RuhsatRepository : IRuhsatRepository
    {
        private readonly RuhsatDbContext _context;

        public RuhsatRepository(RuhsatDbContext context)
        {
            _context = context;
        }

        public async Task<List<Ruhsat>> GetAllAsync()
        {
            return await _context.Ruhsatlar
                .Include(r => r.FaaliyetKonusu)
                .Include(r => r.RuhsatTuru)
                .Include(r => r.RuhsatSinifi)
                .ToListAsync();
        }

        public async Task<Ruhsat> GetByIdAsync(int id)
        {
            return await _context.Ruhsatlar
                .Include(r => r.FaaliyetKonusu)
                .Include(r => r.RuhsatTuru)
                .Include(r => r.RuhsatSinifi)
                .FirstOrDefaultAsync(r => r.Id == id);
        }


        public async Task AddAsync(Ruhsat ruhsat)
        {
            await _context.Ruhsatlar.AddAsync(ruhsat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ruhsat updatedEntity)
        {
            var existingEntity = await _context.Ruhsatlar.FindAsync(updatedEntity.Id);
            if (existingEntity == null)
                return;

            // Tüm güncellenebilir alanları var olan entity'ye set et
            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Ruhsatlar.FindAsync(id);
            if (entity != null)
            {
                _context.Ruhsatlar.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
