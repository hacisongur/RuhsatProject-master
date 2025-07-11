using AutoMapper;
using RuhsaProject.DTOs.DashboardCardDtos;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.Business.IServices;
using RuhsatProject.Core.Interfaces;
using RuhsatProject.DataAccess.Contexts;  // Add this namespace
using RuhsatProject.DTOs.Ruhsat;
using RuhsatProject.Entities.Concrete;

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
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                // Ana ruhsat kaydını oluştur
                var ruhsat = _mapper.Map<Ruhsat>(dto);
                await _ruhsatRepository.AddAsync(ruhsat);
                await _dbContext.SaveChangesAsync();

                // DepoBilgileri'ni ekle
                if (dto.DepoBilgileri != null && dto.DepoBilgileri.Any())
                {
                    foreach (var depoBilgiDto in dto.DepoBilgileri)
                    {
                        var depoBilgi = new DepoBilgi
                        {
                            RuhsatId = ruhsat.Id,
                            DepoId = depoBilgiDto.DepoId,
                            DepoAdi = depoBilgiDto.DepoAdi,
                            Bilgi = depoBilgiDto.Bilgi
                        };
                        await _dbContext.DepoBilgileri.AddAsync(depoBilgi);
                    }
                    await _dbContext.SaveChangesAsync();
                }

                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task UpdateAsync(RuhsatDto dto)
        {
            var entity = _mapper.Map<Ruhsat>(dto);
            await _ruhsatRepository.UpdateAsync(entity);

            // Eski DepoBilgileri'ni sil
            var eskiDepolar = _dbContext.DepoBilgileri.Where(x => x.RuhsatId == dto.Id);
            _dbContext.DepoBilgileri.RemoveRange(eskiDepolar);

            // Yeni DepoBilgileri'ni ekle
            if (dto.DepoBilgileri != null && dto.DepoBilgileri.Count > 0)
            {
                foreach (var depoBilgiDto in dto.DepoBilgileri)
                {
                    var depoBilgi = new DepoBilgi
                    {
                        RuhsatId = dto.Id,
                        DepoId = depoBilgiDto.DepoId,
                        DepoAdi = depoBilgiDto.DepoAdi,
                        Bilgi = depoBilgiDto.Bilgi
                    };
                    await _dbContext.DepoBilgileri.AddAsync(depoBilgi);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _ruhsatRepository.DeleteAsync(id);
        }
        public async Task<List<RuhsatDto>> SearchAsync(string term)
        {
            var results = await _ruhsatRepository.SearchAsync(term);
            return _mapper.Map<List<RuhsatDto>>(results);
        }
        public async Task<IList<RuhsatDto>> GetByActiveStatusAsync(bool isActive)
        {
            var ruhsatlar = await _ruhsatRepository.GetAllAsync(r => r.IsActive.HasValue && r.IsActive.Value == isActive);
            return _mapper.Map<IList<RuhsatDto>>(ruhsatlar);
        }

        public async Task<List<DashboardCardDto>> GetDashboardCardsAsync()
        {
            var all = await _ruhsatRepository.GetAllAsync();
            var now = DateTime.Now;

            var total = all.Count;
            var active = all.Count(x => x.IsActive.HasValue && x.IsActive.Value);
            var passive = all.Count(x => x.IsActive.HasValue && !x.IsActive.Value);
            var lastMonth = all.Count(x => x.CreatedDate >= now.AddMonths(-1));

            return new List<DashboardCardDto>
    {
        new()
        {
            Title = "Toplam Ruhsat",
            Value = total,
            Icon = "ti-home",
            BadgeColor = "primary",
            ChangeText = $"+{total}",
            ChangeNote = "Tüm kayıtlar"
        },
        new()
        {
            Title = "Aktif Ruhsatlar",
            Value = active,
            Icon = "ti-check",
            BadgeColor = "success",
            ChangeText = $"+{active}",
            ChangeNote = "Aktif durumda"
        },
        new()
        {
            Title = "Pasif Ruhsatlar",
            Value = passive,
            Icon = "ti-x",
            BadgeColor = "danger",
            ChangeText = $"-{passive}",
            ChangeNote = "Pasif durumda"
        },
        new()
        {
            Title = "Son 1 Ayda Eklenen",
            Value = lastMonth,
            Icon = "ti-calendar",
            BadgeColor = "info",
            ChangeText = $"+{lastMonth}",
            ChangeNote = "Son 30 gün"
        }
    };
        }


    }
}
