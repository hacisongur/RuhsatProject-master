﻿

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace RuhsaProject.DTOs.UserDtos
{
    public class UserLoginDto
    {
        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} Kullanıcı adı boş olamaz.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "{0} Kullanıcı adı boş olamaz.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır.")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
}
