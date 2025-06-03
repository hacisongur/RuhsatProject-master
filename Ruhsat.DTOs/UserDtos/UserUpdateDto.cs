

using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RuhsaProject.DTOs.UserDtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı boş olamaz.")]
        [MaxLength(50)]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "E-Posta boş olamaz.")]
        [EmailAddress]
        [DisplayName("E-Posta")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Telefon numarası boş olamaz.")]
        [MaxLength(13), MinLength(13)]
        [DisplayName("Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [DisplayName("Yeni Resim")]
        public IFormFile? Picture { get; set; }

        public string? ExistingPicturePath { get; set; }
    }
}
