using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class RuhsatImzaMap : IEntityTypeConfiguration<RuhsatImza>
    {
        public void Configure(EntityTypeBuilder<RuhsatImza> builder)
        {
            builder.ToTable("RuhsatImzalar");

            builder.HasKey(ri => ri.Id);  // Id zorunludur, nullable değil

            builder.Property(ri => ri.AdSoyad)
                   .HasMaxLength(100)
                   .IsRequired(false);  // Nullable alan (null olabilir)

            builder.Property(ri => ri.Unvan1)
                   .HasMaxLength(100)
                   .IsRequired(false);  // Nullable alan (null olabilir)

            builder.Property(ri => ri.Unvan2)
                   .HasMaxLength(100)
                   .IsRequired(false);  // Nullable alan (null olabilir)

      
        }
    }
}
