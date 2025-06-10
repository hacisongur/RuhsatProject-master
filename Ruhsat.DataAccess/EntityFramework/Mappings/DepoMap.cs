
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsaProject.Entities.Concrete;

namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class DepoMap : IEntityTypeConfiguration<Depo>
    {
        public void Configure(EntityTypeBuilder<Depo> builder)
        {
            builder.ToTable("Depolar");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Adi)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(d => d.RuhsatSinifi)
                .WithMany()  // RuhsatSinifi tarafında ICollection<Depo> tanımlamadık, WithMany() boş bırakılır.
                .HasForeignKey(d => d.RuhsatSinifiId);

            // Add any additional configurations for Depo if needed
        }
    }
}
