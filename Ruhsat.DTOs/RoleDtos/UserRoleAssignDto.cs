
namespace RuhsaProject.DTOs.RoleDtos
{
    public class UserRoleAssignDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<RoleAssignDto> RoleAssignDtos { get; set; } = new();
    }
}
