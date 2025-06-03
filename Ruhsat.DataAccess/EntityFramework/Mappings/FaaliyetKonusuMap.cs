using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsatProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class FaaliyetKonusuMap : IEntityTypeConfiguration<FaaliyetKonusu>
    {
        public void Configure(EntityTypeBuilder<FaaliyetKonusu> builder)
        {
            builder.ToTable("FaaliyetKonulari");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Add any additional configurations for FaaliyetKonusu if needed
        }
    }
}
