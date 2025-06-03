
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RuhsaProject.DTOs.RoleDtos
{
    public class RoleAddDto
    {
        [DisplayName("Rol Adı")]
        [Required(ErrorMessage = "{0} alanı boş olamaz.")]
        [MaxLength(100, ErrorMessage = "{0} en fazla {1} karakter olabilir.")]
        public string Name { get; set; }
    }
}
