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

            builder.HasMany(e => e.Tags)
                .WithMany(e => e.Exams)
                .UsingEntity("ExamTag",
                    l => l.HasOne(typeof(Tag)).WithMany().OnDelete(DeleteBehavior.Cascade).HasForeignKey("TagId").HasPrincipalKey(nameof(Tag.Id)),
                    r => r.HasOne(typeof(Exam)).WithMany().OnDelete(DeleteBehavior.Cascade).HasForeignKey("ExamId").HasPrincipalKey(nameof(Exam.Id)),
                    etb =>
                    {
                        etb.Property("TagId").IsRequired().HasColumnType(nameof(SqlDbType.Int));
                        etb.Property("ExamId").IsRequired().HasColumnType(nameof(SqlDbType.Int));
                        etb.HasKey("TagId", "ExamId");
                    }
                 );
        }
    }
}
