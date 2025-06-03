using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsatProject.Entities.Concrete;

namespace RuhsatProject.DataAccess.EntityFramework.Mappings
{
    public class RuhsatTuruMap : IEntityTypeConfiguration<RuhsatTuru>
    {
        public void Configure(EntityTypeBuilder<RuhsatTuru> builder)
        {
            builder.ToTable("RuhsatTurleri");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Name)
                .HasMaxLength(100)
                .IsRequired();

            // Add any additional configurations for RuhsatTuru if needed
        }
    }
}
