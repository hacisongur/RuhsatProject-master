using RuhsaProject.DTOs.RuhsatTuruDtos;
using System.ComponentModel.DataAnnotations;

namespace RuhsaProject.DTOs.RuhsatSinifiDtos
{
    public class RuhsatSinifiDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Ruhsat Sınıfı Adı zorunludur.")]
        public string Name { get; set; }  // 1. sınıf, 2. sınıf vb.

        [Required(ErrorMessage = "Ruhsat Türü seçimi zorunludur.")]
        public int? RuhsatTuruId { get; set; }  // RuhsatTuru'ya ait olan sınıf
        public RuhsatTuruDto? RuhsatTuru { get; set; }  // RuhsatTuru ile ilişki
    }
}
