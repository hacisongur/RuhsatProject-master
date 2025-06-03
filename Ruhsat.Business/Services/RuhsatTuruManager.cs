using AutoMapper;
using RuhsaProject.DTOs.RuhsatTuruDtos;
using RuhsatProject.Business.IServices;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Business.Services
{
    public class RuhsatTuruManager : IRuhsatTuruService
    {
        private readonly IRuhsatTuruRepository _ruhsatTuruRepository;
        private readonly IMapper _mapper;

        public RuhsatTuruManager(IRuhsatTuruRepository ruhsatTuruRepository, IMapper mapper)
        {
            _ruhsatTuruRepository = ruhsatTuruRepository;
            _mapper = mapper;
        }

        public async Task<List<RuhsatTuruDto>> GetAllAsync()
        {
            var ruhsatTurleri = await _ruhsatTuruRepository.GetAllAsync();
            return _mapper.Map<List<RuhsatTuruDto>>(ruhsatTurleri);
        }

        public async Task<RuhsatTuruDto> GetByIdAsync(int id)
        {
            var ruhsatTuru = await _ruhsatTuruRepository.GetByIdAsync(id);
            return _mapper.Map<RuhsatTuruDto>(ruhsatTuru);
        }

        public async Task AddAsync(RuhsatTuruDto dto)
        {
            var entity = _mapper.Map<RuhsatTuru>(dto);
            await _ruhsatTuruRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(RuhsatTuruDto dto)
        {
            var entity = _mapper.Map<RuhsatTuru>(dto);
            await _ruhsatTuruRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ruhsatTuruRepository.DeleteAsync(id);
        }
    }
}
