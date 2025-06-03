using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RuhsaProject.DataAccess.EntityFramework.Mappings;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.DataAccess.EntityFramework.Mappings;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.Contexts
{
    public class RuhsatDbContext : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public RuhsatDbContext(DbContextOptions<RuhsatDbContext> options) : base(options) { }

        public DbSet<Ruhsat> Ruhsatlar { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<FaaliyetKonusu> FaaliyetKonulari { get; set; }
        public DbSet<RuhsatTuru> RuhsatTurleri { get; set; }
        public DbSet<RuhsatSinifi> RuhsatSiniflari { get; set; }
        public DbSet<RuhsatImza> RuhsatImzalar { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Ruhsat - FaaliyetKonusu
            modelBuilder.Entity<Ruhsat>()
                .HasOne(r => r.FaaliyetKonusu)
                .WithMany()
                .HasForeignKey(r => r.FaaliyetKonusuId);

            // Ruhsat - RuhsatTuru
            modelBuilder.Entity<Ruhsat>()
                .HasOne(r => r.RuhsatTuru)
                .WithMany()
                .HasForeignKey(r => r.RuhsatTuruId);

            // RuhsatSinifi - RuhsatTuru
            modelBuilder.Entity<RuhsatSinifi>()
                .HasOne(rs => rs.RuhsatTuru)
                .WithMany()
                .HasForeignKey(rs => rs.RuhsatTuruId);

            // Ruhsat - RuhsatSinifi
            modelBuilder.Entity<Ruhsat>()
                .HasOne(r => r.RuhsatSinifi)
                .WithMany()
                .HasForeignKey(r => r.RuhsatSinifiId);
            // Ruhsat - RuhsatImza (bire bir ilişki, Ruhsat tablosunda FK var)
            modelBuilder.Entity<Ruhsat>()
        .HasOne(r => r.RuhsatImza)
        .WithOne()
        .HasForeignKey<Ruhsat>(r => r.RuhsatImzaId)
        .OnDelete(DeleteBehavior.SetNull); // Silinince ilişkili RuhsatImzaId null yapılacak




            // Mapping sınıfları
            modelBuilder.ApplyConfiguration(new RuhsatMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserClaimMap());
            modelBuilder.ApplyConfiguration(new UserRoleMap());
            modelBuilder.ApplyConfiguration(new UserLoginMap());
            modelBuilder.ApplyConfiguration(new RoleClaimMap());
            modelBuilder.ApplyConfiguration(new UserTokenMap());
            modelBuilder.ApplyConfiguration(new PermissionMap());
            modelBuilder.ApplyConfiguration(new RolePermissionMap());
            modelBuilder.ApplyConfiguration(new LogEntryMap());
            modelBuilder.ApplyConfiguration(new RuhsatImzaMap());
        }
    }
}
