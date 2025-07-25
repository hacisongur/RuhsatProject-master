﻿using AutoMapper;
using RuhsaProject.DTOs.DepoDtos;
using RuhsaProject.DTOs.FaaliyetKonusuDtos;
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
                .ForMember(dest => dest.ScannedFilePath, opt => opt.MapFrom(src => src.ScannedFilePath))
                .ForMember(dest => dest.DepoBilgileri, opt => opt.MapFrom(src => src.DepoBilgileri));

            // RuhsatDto → Ruhsat
            CreateMap<RuhsatDto, Ruhsat>()
                .ForMember(dest => dest.FaaliyetKonusu, opt => opt.Ignore())
                .ForMember(dest => dest.RuhsatTuru, opt => opt.Ignore())
                .ForMember(dest => dest.RuhsatSinifi, opt => opt.Ignore())
                .ForMember(dest => dest.ScannedFilePath, opt => opt.MapFrom(src => src.ScannedFilePath))
                .ForMember(dest => dest.DepoBilgileri, opt => opt.Ignore());

            // ALT NESNE MAP'LERİ (Eksikse hata verir!)
            CreateMap<FaaliyetKonusu, FaaliyetKonusuDto>().ReverseMap();
            CreateMap<RuhsatTuru, RuhsatTuruDto>().ReverseMap();
            CreateMap<RuhsatSinifi, RuhsatSinifiDto>().ReverseMap();
            CreateMap<Depo, DepoDto>().ReverseMap();
            CreateMap<DepoBilgiDto, DepoBilgi>().ReverseMap();
        }
    }
}
