using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuhsaProject.Entities.Concrete;
namespace RuhsaProject.DataAccess.EntityFramework.Mappings
{
    public class LogEntryMap : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            builder.ToTable("LogEntries");

            builder.HasKey(l => l.Id);

            builder.Property(l => l.UserId).IsRequired().HasMaxLength(450);
            builder.Property(l => l.Action).IsRequired().HasMaxLength(100);
            builder.Property(l => l.EntityName).IsRequired().HasMaxLength(100);
            builder.Property(l => l.Timestamp).IsRequired();
            builder.Property(l => l.Description).HasMaxLength(1000);
            builder.Property(l => l.IpAddress).HasMaxLength(50);
        }
    }
}
