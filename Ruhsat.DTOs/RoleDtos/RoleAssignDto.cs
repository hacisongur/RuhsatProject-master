
namespace RuhsaProject.DTOs.RoleDtos
{
    public class RoleAssignDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool HasRole { get; set; } // Kullanıcının bu role sahip olup olmadığını belirler
    }
}
