using RuhsaProject.DTOs.Base;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DTOs.RoleDtos
{
    public class RoleListDto:DtoGetBase
    {
        public IList<Role> Roles { get; set; }
    }
}
