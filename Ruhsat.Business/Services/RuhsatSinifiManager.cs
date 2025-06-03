using AutoMapper;
using RuhsaProject.DTOs.RuhsatSinifiDtos;
using RuhsatProject.Business.IServices;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Business.Services
{
    public class RuhsatSinifiManager : IRuhsatSinifiService
    {
        private readonly IRuhsatSinifiRepository _ruhsatSinifiRepository;
        private readonly IMapper _mapper;

        public RuhsatSinifiManager(IRuhsatSinifiRepository ruhsatSinifiRepository, IMapper mapper)
        {
            _ruhsatSinifiRepository = ruhsatSinifiRepository;
            _mapper = mapper;
        }

        public async Task<List<RuhsatSinifiDto>> GetAllAsync()
        {
            var ruhsatSiniflari = await _ruhsatSinifiRepository.GetAllAsync();
            return _mapper.Map<List<RuhsatSinifiDto>>(ruhsatSiniflari);
        }

        public async Task<RuhsatSinifiDto> GetByIdAsync(int id)
        {
            var ruhsatSinifi = await _ruhsatSinifiRepository.GetByIdAsync(id);
            return _mapper.Map<RuhsatSinifiDto>(ruhsatSinifi);
        }

        public async Task AddAsync(RuhsatSinifiDto dto)
        {
            try
            {
                Console.WriteLine($"RuhsatSinifiManager.AddAsync - Gelen DTO: Name={dto.Name}, RuhsatTuruId={dto.RuhsatTuruId}");

                var entity = _mapper.Map<RuhsatSinifi>(dto);
                Console.WriteLine($"Entity oluşturuldu: Name={entity.Name}, RuhsatTuruId={entity.RuhsatTuruId}");

                await _ruhsatSinifiRepository.AddAsync(entity);
                Console.WriteLine("Repository.AddAsync başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RuhsatSinifiManager.AddAsync - Hata: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task UpdateAsync(RuhsatSinifiDto dto)
        {
            var entity = _mapper.Map<RuhsatSinifi>(dto);
            await _ruhsatSinifiRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _ruhsatSinifiRepository.DeleteAsync(id);
        }
    }
}
