
namespace RuhsaProject.Entities.Concrete
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }  // Ör: "User.Create"
        public string Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}
