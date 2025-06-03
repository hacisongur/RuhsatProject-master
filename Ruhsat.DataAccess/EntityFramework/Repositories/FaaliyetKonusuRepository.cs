using Microsoft.EntityFrameworkCore;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.EntityFramework.Repositories
{
    public class FaaliyetKonusuRepository : IFaaliyetKonusuRepository
    {
        private readonly RuhsatDbContext _context;

        public FaaliyetKonusuRepository(RuhsatDbContext context)
        {
            _context = context;
        }

        public async Task<List<FaaliyetKonusu>> GetAllAsync()
        {
            return await _context.FaaliyetKonulari.ToListAsync();
        }

        public async Task<FaaliyetKonusu> GetByIdAsync(int id)
        {
            return await _context.FaaliyetKonulari.FindAsync(id);
        }

        public async Task AddAsync(FaaliyetKonusu faaliyetKonusu)
        {
            await _context.FaaliyetKonulari.AddAsync(faaliyetKonusu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FaaliyetKonusu updatedEntity)
        {
            var existingEntity = await _context.FaaliyetKonulari.FindAsync(updatedEntity.Id);
            if (existingEntity == null)
                return;

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.FaaliyetKonulari.FindAsync(id);
            if (entity != null)
            {
                _context.FaaliyetKonulari.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
