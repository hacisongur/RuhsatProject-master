using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.EntityFramework.Mappings
{
    public class RuhsatMap : IEntityTypeConfiguration<Ruhsat>
    {
        public void Configure(EntityTypeBuilder<Ruhsat> builder)
        {
            builder.ToTable("Ruhsatlar");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.RuhsatNo).HasMaxLength(50).IsRequired();
            builder.Property(r => r.VerilisTarihi).IsRequired();

            builder.Property(r => r.TcKimlikNo).HasMaxLength(11);
            builder.Property(r => r.Adi).HasMaxLength(50);
            builder.Property(r => r.Soyadi).HasMaxLength(50);
            builder.Property(r => r.IsyeriUnvani).HasMaxLength(150);

            builder.HasOne(r => r.FaaliyetKonusu)
                .WithMany()
                .HasForeignKey(r => r.FaaliyetKonusuId);

            builder.HasOne(r => r.RuhsatTuru)
                .WithMany()
                .HasForeignKey(r => r.RuhsatTuruId);

            builder.HasOne(r => r.RuhsatSinifi) // Ruhsat ile RuhsatSinifi ilişkisi
                .WithMany()
                .HasForeignKey(r => r.RuhsatSinifiId); // RuhsatSinifiId foreign key'i

            builder.HasOne(r => r.RuhsatImza)  // Ruhsat ile RuhsatImza ilişkisi
           .WithOne()   // One-to-one ilişki
           .HasForeignKey<Ruhsat>(r => r.RuhsatImzaId) // RuhsatImzaId foreign key
           .OnDelete(DeleteBehavior.SetNull);  // İlgili RuhsatImza silindiğinde, RuhsatImzaId null yapılır

            builder.Property(r => r.Adres).HasMaxLength(300);
            builder.Property(r => r.Not).HasMaxLength(500);
            builder.Property(r => r.PhotoPath).HasMaxLength(250);
            builder.Property(r => r.ScannedFilePath).HasMaxLength(250);

            builder.Property(r => r.Ada).HasMaxLength(20);
            builder.Property(r => r.Parsel).HasMaxLength(20);
            builder.Property(r => r.Pafta).HasMaxLength(20);

            builder.Property(r => r.IsActive).HasDefaultValue(true);

        }
    }
}
