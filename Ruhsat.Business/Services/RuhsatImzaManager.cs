using RuhsaProject.Business.IServices;
using RuhsaProject.Core.Interfaces;
using RuhsaProject.DataAccess.EntityFramework.Repositories;
using RuhsaProject.Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RuhsaProject.Business.Services
{
    public class RuhsatImzaManager : IRuhsatImzaService
    {
        private readonly IRuhsatImzaRepository _ruhsatImzaRepository;

        public RuhsatImzaManager(IRuhsatImzaRepository ruhsatImzaRepository)
        {
            _ruhsatImzaRepository = ruhsatImzaRepository;
        }

        public async Task<List<RuhsatImza>> GetAllAsync()
        {
            return await _ruhsatImzaRepository.GetAllAsync();
        }

        public async Task<RuhsatImza> GetByIdAsync(int id)
        {
            return await _ruhsatImzaRepository.GetByIdAsync(id);
        }

        public async Task AddAsync(RuhsatImza entity)
        {
            await _ruhsatImzaRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(RuhsatImza entity)
        {
            await _ruhsatImzaRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ruhsatImzaRepository.DeleteAsync(id);
        }
    }
}
