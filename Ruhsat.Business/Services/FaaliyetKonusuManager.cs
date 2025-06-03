using AutoMapper;
using RuhsaProject.DTOs.FaaliyetKonusuDtos;
using RuhsatProject.Business.IServices;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Business.Services
{
    public class FaaliyetKonusuManager : IFaaliyetKonusuService
    {
        private readonly IFaaliyetKonusuRepository _faaliyetKonusuRepository;
        private readonly IMapper _mapper;

        public FaaliyetKonusuManager(IFaaliyetKonusuRepository faaliyetKonusuRepository, IMapper mapper)
        {
            _faaliyetKonusuRepository = faaliyetKonusuRepository;
            _mapper = mapper;
        }

        public async Task<List<FaaliyetKonusuDto>> GetAllAsync()
        {
            var faaliyetKonulari = await _faaliyetKonusuRepository.GetAllAsync();
            return _mapper.Map<List<FaaliyetKonusuDto>>(faaliyetKonulari);
        }

        public async Task<FaaliyetKonusuDto> GetByIdAsync(int id)
        {
            var faaliyetKonusu = await _faaliyetKonusuRepository.GetByIdAsync(id);
            return _mapper.Map<FaaliyetKonusuDto>(faaliyetKonusu);
        }

        public async Task AddAsync(FaaliyetKonusuDto dto)
        {
            var entity = _mapper.Map<FaaliyetKonusu>(dto);
            await _faaliyetKonusuRepository.AddAsync(entity);
        }

        public async Task UpdateAsync(FaaliyetKonusuDto dto)
        {
            var entity = _mapper.Map<FaaliyetKonusu>(dto);
            await _faaliyetKonusuRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _faaliyetKonusuRepository.DeleteAsync(id);
        }
    }
}
