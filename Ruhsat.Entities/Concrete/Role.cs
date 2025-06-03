
using Microsoft.AspNetCore.Identity;

namespace RuhsaProject.Entities.Concrete
{
    public class Role : IdentityRole<int>
    {
        public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();

    }
}
