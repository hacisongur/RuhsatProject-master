using Microsoft.EntityFrameworkCore;
using RuhsaProject.Core.Interfaces;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.DataAccess.Contexts;

namespace RuhsaProject.DataAccess.EntityFramework.Repositories
{
    public class DepoRepository : IDepoRepository
    {
        private readonly RuhsatDbContext _context;

        public DepoRepository(RuhsatDbContext context)
        {
            _context = context;
        }

        public async Task<List<Depo>> GetAllAsync()
        {
            return await _context.Depolar
                .Include(d => d.RuhsatSinifi) // RuhsatSinifi dahil olsun
                .ToListAsync();
        }

        public async Task<Depo> GetByIdAsync(int id)
        {
            return await _context.Depolar
                .Include(d => d.RuhsatSinifi) // RuhsatSinifi dahil
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task AddAsync(Depo depo)
        {
            await _context.Depolar.AddAsync(depo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Depo updatedEntity)
        {
            var existingEntity = await _context.Depolar.FindAsync(updatedEntity.Id);
            if (existingEntity == null)
                return;

            _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Depolar.FindAsync(id);
            if (entity != null)
            {
                _context.Depolar.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
