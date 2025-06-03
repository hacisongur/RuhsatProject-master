

namespace RuhsaProject.DTOs.RoleDtos
{
    public class RolePermissionAssignDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public List<PermissionAssignDto> PermissionAssignDtos { get; set; } = new();
    }
}
