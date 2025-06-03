using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class PermissionMap : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("Permissions");

            builder.HasKey(p => p.Id);

            // İzin adı için maksimum uzunluk
            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Açıklama için maksimum uzunluk
            builder.Property(p => p.Description)
                .HasMaxLength(250);

            // Permission ile RolePermission arasındaki ilişkiyi tanımlıyoruz
            builder.HasMany(p => p.RolePermissions)
                .WithOne(rp => rp.Permission)
                .HasForeignKey(rp => rp.PermissionId)
                .IsRequired();

            // Seed verileri ekliyoruz
            builder.HasData(
                new Permission { Id = 1, Name = "User.Create", Description = "Kullanıcı ekleyebilir" },
                new Permission { Id = 2, Name = "User.Read", Description = "Kullanıcıları görüntüleyebilir" },
                new Permission { Id = 3, Name = "User.Update", Description = "Kullanıcı güncelleyebilir" },
                new Permission { Id = 4, Name = "User.Delete", Description = "Kullanıcı silebilir" },

                new Permission { Id = 5, Name = "Role.Create", Description = "Rol ekleyebilir" },
                new Permission { Id = 6, Name = "Role.Read", Description = "Rolleri görüntüleyebilir" },
                new Permission { Id = 7, Name = "Role.Update", Description = "Rol güncelleyebilir" },
                new Permission { Id = 8, Name = "Role.Delete", Description = "Rol silebilir" },

                new Permission { Id = 9, Name = "Ruhsat.Create", Description = "Ruhsat ekleyebilir" },
                new Permission { Id = 10, Name = "Ruhsat.Read", Description = "Ruhsatları görüntüleyebilir" },
                new Permission { Id = 11, Name = "Ruhsat.Update", Description = "Ruhsat güncelleyebilir" },
                new Permission { Id = 12, Name = "Ruhsat.Delete", Description = "Ruhsat silebilir" },

                // RuhsatTuru Permissions
                new Permission { Id = 13, Name = "RuhsatTuru.Create", Description = "RuhsatTuru ekleyebilir" },
                new Permission { Id = 14, Name = "RuhsatTuru.Read", Description = "RuhsatTuru görüntüleyebilir" },
                new Permission { Id = 15, Name = "RuhsatTuru.Update", Description = "RuhsatTuru güncelleyebilir" },
                new Permission { Id = 16, Name = "RuhsatTuru.Delete", Description = "RuhsatTuru silebilir" },

                // RuhsatSinifi Permissions
                new Permission { Id = 17, Name = "RuhsatSinifi.Create", Description = "RuhsatSinifi ekleyebilir" },
                new Permission { Id = 18, Name = "RuhsatSinifi.Read", Description = "RuhsatSinifi görüntüleyebilir" },
                new Permission { Id = 19, Name = "RuhsatSinifi.Update", Description = "RuhsatSinifi güncelleyebilir" },
                new Permission { Id = 20, Name = "RuhsatSinifi.Delete", Description = "RuhsatSinifi silebilir" },

                // FaaliyetKonusu Permissions
                new Permission { Id = 21, Name = "FaaliyetKonusu.Create", Description = "FaaliyetKonusu ekleyebilir" },
                new Permission { Id = 22, Name = "FaaliyetKonusu.Read", Description = "FaaliyetKonusu görüntüleyebilir" },
                new Permission { Id = 23, Name = "FaaliyetKonusu.Update", Description = "FaaliyetKonusu güncelleyebilir" },
                new Permission { Id = 24, Name = "FaaliyetKonusu.Delete", Description = "FaaliyetKonusu silebilir" }
            );
        }
    }
}
