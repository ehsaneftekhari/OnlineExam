using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Mappings
{
    public class ExamTagEntityTypeConfiguration : IEntityTypeConfiguration<ExamTag>
    {
        public void Configure(EntityTypeBuilder<ExamTag> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Exam)
                .WithMany(e => e.ExamTags)
                .HasForeignKey(e => e.ExamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Tag)
                .WithMany(e => e.ExamTags)
                .HasForeignKey(e => e.TagId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
