using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RuhsaProject.DTOs.RuhsatImzaDtos
{
    public class RuhsatImzaDto
    {
        public int Id { get; set; }

        [DisplayName("Ad Soyad")]
        [Required(ErrorMessage = "Ad Soyad alanı zorunludur.")]
        [MaxLength(100, ErrorMessage = "Ad Soyad en fazla 100 karakter olabilir.")]
        public string? AdSoyad { get; set; }

        [DisplayName("Ünvan 1")]
        [MaxLength(100, ErrorMessage = "Ünvan 1 en fazla 100 karakter olabilir.")]
        public string? Unvan1 { get; set; }

        [DisplayName("Ünvan 2")]
        [MaxLength(150, ErrorMessage = "Ünvan 2 en fazla 150 karakter olabilir.")]
        public string? Unvan2 { get; set; }
    }
}
