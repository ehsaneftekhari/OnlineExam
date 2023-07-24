using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    public class ExamEntityTypeConfiguration : IEntityTypeConfiguration<Exam>
    {
        public void Configure(EntityTypeBuilder<Exam> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(p => p.Start)
               .IsRequired()
               .HasColumnType<DateTime>(nameof(SqlDbType.DateTime));

            builder.Property(p => p.End)
               .IsRequired()
               .HasColumnType<DateTime>(nameof(SqlDbType.DateTime));

            builder.Property(p => p.Published)
               .IsRequired()
               .HasColumnType<bool>(nameof(SqlDbType.Bit));

            builder.ToTable(tableBuilder =>
                    {
                        tableBuilder.IsTemporal();
                    });
        }
    }
}
