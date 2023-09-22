using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class AnswerEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Answer>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasOne(x => x.ExamUser)
                .WithMany()
                .HasForeignKey(x => x.ExamUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Content)
                .IsRequired()
                .HasMaxLength(4000);
        }
    }
}
