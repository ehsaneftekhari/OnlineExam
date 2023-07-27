using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(p => p.Description!)
                .HasMaxLength(1024)
               .HasColumnType<string>(nameof(SqlDbType.NVarChar))
               .IsRequired(false);
        }
    }
}
