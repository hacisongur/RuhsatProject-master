using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.EntityFramework.Mappings
{
    public class RuhsatSinifiMap : IEntityTypeConfiguration<RuhsatSinifi>
    {
        public void Configure(EntityTypeBuilder<RuhsatSinifi> builder)
        {
            builder.ToTable("RuhsatSiniflari");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(r => r.RuhsatTuru)
                .WithMany()
                .HasForeignKey(r => r.RuhsatTuruId);

            // Add any additional configurations for RuhsatSinifi if needed
        }
    }
}
