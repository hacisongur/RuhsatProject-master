using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RuhsaProject.Core.Interfaces;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.Business.IServices;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;  // Add this namespace
using RuhsatProject.DTOs.Ruhsat;
using RuhsatProject.Entities.Concrete;
using System.Linq.Expressions;

namespace RuhsatProject.Business.Services
{
    public class RuhsatManager : IRuhsatService
    {
        private readonly IRuhsatRepository _ruhsatRepository;
        private readonly IMapper _mapper;
        private readonly RuhsatDbContext _dbContext;  
        public RuhsatManager(IRuhsatRepository ruhsatRepository, IMapper mapper, RuhsatDbContext dbContext)
        {
            _ruhsatRepository = ruhsatRepository;
            _mapper = mapper;
            _dbContext = dbContext;  
        }

        public async Task<List<RuhsatDto>> GetAllAsync()
        {
            var ruhsatlar = await _ruhsatRepository.GetAllAsync();
            return _mapper.Map<List<RuhsatDto>>(ruhsatlar);
        }

        public async Task<RuhsatDto> GetByIdAsync(int id)
        {
            var ruhsat = await _ruhsatRepository.GetByIdAsync(id);
            return _mapper.Map<RuhsatDto>(ruhsat);
        }

        public async Task AddAsync(RuhsatDto dto)
        {
            // 1️⃣ Ruhsat kaydet
            var ruhsatEntity = _mapper.Map<Ruhsat>(dto);
            await _ruhsatRepository.AddAsync(ruhsatEntity);

            // 2️⃣ DepoBilgileri kaydet (eğer varsa)
            if (dto.DepoBilgileri != null && dto.DepoBilgileri.Count > 0)
            {
                foreach (var depoBilgiDto in dto.DepoBilgileri)
                {
                    var depoBilgi = new DepoBilgi
                    {
                        RuhsatId = ruhsatEntity.Id, // Ruhsat FK
                        DepoId = depoBilgiDto.DepoId,
                        DepoAdi = depoBilgiDto.DepoAdi,
                        Bilgi = depoBilgiDto.Bilgi
                    };

                    await _dbContext.DepoBilgileri.AddAsync(depoBilgi);
                }

                // DepoBilgileri için SaveChanges
                await _dbContext.SaveChangesAsync();
            }
        }


        public async Task UpdateAsync(RuhsatDto dto)
        {
            var entity = _mapper.Map<Ruhsat>(dto);
            await _ruhsatRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ruhsatRepository.DeleteAsync(id);
        }

        
    }
}
