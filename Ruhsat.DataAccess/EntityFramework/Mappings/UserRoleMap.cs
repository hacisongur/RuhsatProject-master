

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class UserRoleMap : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");

            builder.HasData(
               new UserRole
               {
                   RoleId = 1,
                   UserId = 1
               },
               new UserRole
               {
                   RoleId = 2,
                   UserId = 2
               }
           );
        }
    }
}
