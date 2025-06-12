using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsaProject.Entities.Concrete;
using RuhsatProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class DepoBilgiMap : IEntityTypeConfiguration<DepoBilgi>
    {
        public void Configure(EntityTypeBuilder<DepoBilgi> builder)
        {
            builder.ToTable("DepoBilgileri");

            builder.HasKey(db => db.Id);

            builder.Property(db => db.DepoAdi)
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(db => db.Bilgi)
                .HasMaxLength(1000)
                .IsRequired();

            // Ruhsat ilişkisi
            builder.HasOne(db => db.Ruhsat)
                   .WithMany(r => r.DepoBilgileri)
                   .HasForeignKey(db => db.RuhsatId)
                   .OnDelete(DeleteBehavior.Cascade); // Ruhsat silinince DepoBilgileri de silinir

            // Depo ilişkisi
            builder.HasOne(db => db.Depo)
                   .WithMany()
                   .HasForeignKey(db => db.DepoId)
                   .OnDelete(DeleteBehavior.Restrict); // Depo silinemezse DepoBilgi bağlıysa hata verir
        }
    }
}
