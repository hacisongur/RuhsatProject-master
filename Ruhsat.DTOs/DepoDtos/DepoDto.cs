

using RuhsaProject.DTOs.RuhsatSinifiDtos;
using System.ComponentModel.DataAnnotations;

namespace RuhsaProject.DTOs.DepoDtos
{
    public class DepoDto
    {
        public int Id { get; set; }

        [Display(Name = "Depo Adı")]
        [Required(ErrorMessage = "Depo adı zorunludur.")]
        public string Adi { get; set; }

        [Display(Name = "Ruhsat Sınıfı")]
        [Required(ErrorMessage = "Ruhsat sınıfı seçimi zorunludur.")]
        public int? RuhsatSinifiId { get; set; }

        public RuhsatSinifiDto? RuhsatSinifi { get; set; }  // Navigation DTO
    }
}
