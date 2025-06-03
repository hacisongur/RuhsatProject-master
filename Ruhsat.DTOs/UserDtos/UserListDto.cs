

using RuhsaProject.DTOs.Base;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DTOs.UserDtos
{
    public class UserListDto:DtoGetBase
    {
        public IList<User> Users { get; set; }

    }
}
