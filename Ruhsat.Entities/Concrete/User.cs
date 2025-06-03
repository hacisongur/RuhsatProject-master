

using Microsoft.AspNetCore.Identity;

namespace RuhsaProject.Entities.Concrete
{
    public class User : IdentityUser<int>
    {
        public string Picture { get; set; }
    }
}
