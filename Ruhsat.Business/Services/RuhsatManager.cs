using AutoMapper;
using RuhsatProject.Business.IServices;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DTOs.Ruhsat;
using RuhsatProject.Entities.Concrete;
namespace RuhsatProject.Business.Services
{
    public class RuhsatManager : IRuhsatService
    {
        private readonly IRuhsatRepository _ruhsatRepository;
        private readonly IMapper _mapper;

        public RuhsatManager(IRuhsatRepository ruhsatRepository, IMapper mapper)
        {
            _ruhsatRepository = ruhsatRepository;
            _mapper = mapper;
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
            var entity = _mapper.Map<Ruhsat>(dto);
            await _ruhsatRepository.AddAsync(entity);
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
