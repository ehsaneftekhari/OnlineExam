using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    public class ExamEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Exam>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<Exam> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType(nameof(SqlDbType.NVarChar));

            builder.Property(p => p.Start)
               .IsRequired()
               .HasColumnType(nameof(SqlDbType.DateTime));

            builder.Property(p => p.End)
               .IsRequired()
               .HasColumnType(nameof(SqlDbType.DateTime));

            builder.Property(p => p.Published)
               .IsRequired()
               .HasColumnType(nameof(SqlDbType.Bit));
        }
    }
}
