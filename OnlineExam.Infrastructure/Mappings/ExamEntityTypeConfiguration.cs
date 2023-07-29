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
                .WithMany(t => t.Exams)
                .UsingEntity<ExamTag>(nameof(ExamTag),
                    l => l.HasOne(et => et.Tag).WithMany(t => t.ExamTags).OnDelete(DeleteBehavior.Cascade).HasForeignKey(et => et.TagId).HasPrincipalKey(t => t.Id),
                    r => r.HasOne(et => et.Exam).WithMany(e => e.ExamTags).OnDelete(DeleteBehavior.Cascade).HasForeignKey(et => et.ExamId).HasPrincipalKey(e => e.Id),
                    builder =>
                    {
                        builder.HasKey(nameof(ExamTag.ExamId), nameof(ExamTag.TagId));
                        builder.Property(e => e.TagId).IsRequired().HasColumnType(nameof(SqlDbType.Int));
                        builder.Property(e => e.ExamId).IsRequired().HasColumnType(nameof(SqlDbType.Int));
                    }
                 );
        }
    }
    
    //public class ExamTagEntityTypeConfiguration : IEntityTypeConfiguration<ExamTag>
    //{
    //    public void Configure(EntityTypeBuilder<ExamTag> builder)
    //    {
    //        builder.HasKey("ExamId", "TagId");

    //        builder.Property(e => e.TagId)
    //            .IsRequired()
    //            .HasColumnType(nameof(SqlDbType.Int));

    //        builder.Property(e => e.ExamId)
    //            .IsRequired()
    //            .HasColumnType(nameof(SqlDbType.Int));
    //    }
    //}
}
