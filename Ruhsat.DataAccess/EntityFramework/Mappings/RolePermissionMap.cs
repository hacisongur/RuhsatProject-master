using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class RolePermissionMap : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("RolePermissions");

            builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });

            builder.HasOne(rp => rp.Role)
                   .WithMany(r => r.RolePermissions)
                   .HasForeignKey(rp => rp.RoleId)
                   .IsRequired();

            builder.HasOne(rp => rp.Permission)
                   .WithMany(p => p.RolePermissions)
                   .HasForeignKey(rp => rp.PermissionId)
                   .IsRequired();

            // Seed RolePermission with corresponding RoleId and PermissionId
            builder.HasData(
                // Admin rolüne tüm izinler atanıyor
                new RolePermission { RoleId = 1, PermissionId = 1 },  // User.Create
                new RolePermission { RoleId = 1, PermissionId = 2 },  // User.Read
                new RolePermission { RoleId = 1, PermissionId = 3 },  // User.Update
                new RolePermission { RoleId = 1, PermissionId = 4 },  // User.Delete
                new RolePermission { RoleId = 1, PermissionId = 5 },  // Role.Create
                new RolePermission { RoleId = 1, PermissionId = 6 },  // Role.Read
                new RolePermission { RoleId = 1, PermissionId = 7 },  // Role.Update
                new RolePermission { RoleId = 1, PermissionId = 8 },  // Role.Delete
                new RolePermission { RoleId = 1, PermissionId = 9 },  // Ruhsat.Create
                new RolePermission { RoleId = 1, PermissionId = 10 }, // Ruhsat.Read
                new RolePermission { RoleId = 1, PermissionId = 11 }, // Ruhsat.Update
                new RolePermission { RoleId = 1, PermissionId = 12 }, // Ruhsat.Delete
                new RolePermission { RoleId = 1, PermissionId = 13 }, // RuhsatTuru.Create
                new RolePermission { RoleId = 1, PermissionId = 14 }, // RuhsatTuru.Read
                new RolePermission { RoleId = 1, PermissionId = 15 }, // RuhsatTuru.Update
                new RolePermission { RoleId = 1, PermissionId = 16 }, // RuhsatTuru.Delete
                new RolePermission { RoleId = 1, PermissionId = 17 }, // RuhsatSinifi.Create
                new RolePermission { RoleId = 1, PermissionId = 18 }, // RuhsatSinifi.Read
                new RolePermission { RoleId = 1, PermissionId = 19 }, // RuhsatSinifi.Update
                new RolePermission { RoleId = 1, PermissionId = 20 }, // RuhsatSinifi.Delete
                new RolePermission { RoleId = 1, PermissionId = 21 }, // FaaliyetKonusu.Create
                new RolePermission { RoleId = 1, PermissionId = 22 }, // FaaliyetKonusu.Read
                new RolePermission { RoleId = 1, PermissionId = 23 }, // FaaliyetKonusu.Update
                new RolePermission { RoleId = 1, PermissionId = 24 }, // FaaliyetKonusu.Delete

                // Editor rolüne sınırlı izinler atanıyor
                new RolePermission { RoleId = 2, PermissionId = 1 },  // User.Create
                new RolePermission { RoleId = 2, PermissionId = 2 },  // User.Read
                new RolePermission { RoleId = 2, PermissionId = 3 },  // User.Update
                new RolePermission { RoleId = 2, PermissionId = 5 },  // Role.Create
                new RolePermission { RoleId = 2, PermissionId = 6 },  // Role.Read
                new RolePermission { RoleId = 2, PermissionId = 7 },  // Role.Update
                new RolePermission { RoleId = 2, PermissionId = 9 },  // Ruhsat.Create
                new RolePermission { RoleId = 2, PermissionId = 10 }, // Ruhsat.Read
                new RolePermission { RoleId = 2, PermissionId = 11 }, // Ruhsat.Update
                new RolePermission { RoleId = 2, PermissionId = 13 }, // RuhsatTuru.Create
                new RolePermission { RoleId = 2, PermissionId = 14 }, // RuhsatTuru.Read
                new RolePermission { RoleId = 2, PermissionId = 15 }, // RuhsatTuru.Update
                new RolePermission { RoleId = 2, PermissionId = 17 }, // RuhsatSinifi.Create
                new RolePermission { RoleId = 2, PermissionId = 18 }, // RuhsatSinifi.Read
                new RolePermission { RoleId = 2, PermissionId = 19 }, // RuhsatSinifi.Update
                new RolePermission { RoleId = 2, PermissionId = 21 }, // FaaliyetKonusu.Create
                new RolePermission { RoleId = 2, PermissionId = 22 }, // FaaliyetKonusu.Read
                new RolePermission { RoleId = 2, PermissionId = 23 }  // FaaliyetKonusu.Update
            );
        }
    }
}
