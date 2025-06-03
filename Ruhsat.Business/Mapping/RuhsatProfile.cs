using AutoMapper;
using RuhsaProject.DTOs.FaaliyetKonusuDtos;
using RuhsaProject.DTOs.RuhsatImzaDtos;
using RuhsaProject.DTOs.RuhsatSinifiDtos;
using RuhsaProject.DTOs.RuhsatTuruDtos;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.DTOs.Ruhsat;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.Business.Mapping
{
    public class RuhsatProfile : Profile
    {
        public RuhsatProfile()
        {
            // Ruhsat → RuhsatDto
            CreateMap<Ruhsat, RuhsatDto>()
                .ForMember(dest => dest.FaaliyetKonusu, opt => opt.MapFrom(src => src.FaaliyetKonusu))
                .ForMember(dest => dest.RuhsatTuru, opt => opt.MapFrom(src => src.RuhsatTuru))
                .ForMember(dest => dest.RuhsatSinifi, opt => opt.MapFrom(src => src.RuhsatSinifi))
                .ForMember(dest => dest.RuhsatImza, opt => opt.MapFrom(src => src.RuhsatImza)) // RuhsatImza'yı buraya ekliyoruz
                .ForMember(dest => dest.ScannedFilePath, opt => opt.MapFrom(src => src.ScannedFilePath));

            // RuhsatDto → Ruhsat
            CreateMap<RuhsatDto, Ruhsat>()
                .ForMember(dest => dest.FaaliyetKonusu, opt => opt.Ignore())
                .ForMember(dest => dest.RuhsatTuru, opt => opt.Ignore())
                .ForMember(dest => dest.RuhsatSinifi, opt => opt.Ignore())
                .ForMember(dest => dest.RuhsatImza, opt => opt.Ignore())  // RuhsatImza ilişkisinin eklenmesi
                .ForMember(dest => dest.ScannedFilePath, opt => opt.MapFrom(src => src.ScannedFilePath));

            // ALT NESNE MAP’LERİ (Eksikse hata verir!)
            CreateMap<FaaliyetKonusu, FaaliyetKonusuDto>().ReverseMap();
            CreateMap<RuhsatTuru, RuhsatTuruDto>().ReverseMap();
            CreateMap<RuhsatSinifi, RuhsatSinifiDto>().ReverseMap();
            CreateMap<RuhsatImza, RuhsatImzaDto>().ReverseMap();
        }
    }
}
