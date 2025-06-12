using AutoMapper;
using RuhsaProject.Business.IServices;
using RuhsaProject.Core.Interfaces;
using RuhsaProject.DTOs.DepoDtos;
using RuhsaProject.Entities.Concrete;
using System.Linq.Expressions;

namespace RuhsaProject.Business.Services
{
    public class DepoManager : IDepoService
    {
        private readonly IDepoRepository _depoRepository;
        private readonly IMapper _mapper;

        public DepoManager(IDepoRepository depoRepository, IMapper mapper)
        {
            _depoRepository = depoRepository;
            _mapper = mapper;
        }

        // 1️⃣ Boş hali → tüm depoları getirir
        public async Task<List<DepoDto>> GetAllAsync()
        {
            var depolar = await _depoRepository.GetAllAsync();
            return _mapper.Map<List<DepoDto>>(depolar);
        }

        // 2️⃣ Predicate ile → filtreli getir
        public async Task<List<DepoDto>> GetAllAsync(Expression<Func<Depo, bool>> predicate)
        {
            var depolar = await _depoRepository.GetAllAsync(predicate);
            return _mapper.Map<List<DepoDto>>(depolar);
        }

        public async Task<DepoDto> GetByIdAsync(int id)
        {
            var depo = await _depoRepository.GetByIdAsync(id);
            return _mapper.Map<DepoDto>(depo);
        }

        public async Task AddAsync(DepoDto dto)
        {
            try
            {
                Console.WriteLine($"DepoManager.AddAsync - Gelen DTO: Adi={dto.Adi}, RuhsatSinifiId={dto.RuhsatSinifiId}");

                var entity = _mapper.Map<Depo>(dto);
                Console.WriteLine($"Entity oluşturuldu: Adi={entity.Adi}, RuhsatSinifiId={entity.RuhsatSinifiId}");

                await _depoRepository.AddAsync(entity);
                Console.WriteLine("Repository.AddAsync başarıyla tamamlandı");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DepoManager.AddAsync - Hata: {ex.Message}");
                Console.WriteLine($"Stack Trace: {ex.StackTrace}");
                throw;
            }
        }

        public async Task UpdateAsync(DepoDto dto)
        {
            var entity = _mapper.Map<Depo>(dto);
            await _depoRepository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _depoRepository.DeleteAsync(id);
        }

        // 3️⃣ Depo entity olarak getir → direkt entity lazım çünkü DepoBilgiDto oluşturacağız
        public async Task<List<Depo>> GetListByRuhsatSinifiIdAsync(int ruhsatSinifiId)
        {
            return await _depoRepository.GetAllAsync(x => x.RuhsatSinifiId == ruhsatSinifiId);
        }
    }
}
