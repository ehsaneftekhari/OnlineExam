using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class ExamUserEntityTypeConfiguration : BaseModelEntityTypeConfiguration<ExamUser>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<ExamUser> builder)
        {
            builder.HasOne(x => x.Exam)
                .WithMany(e => e.ExamUsers)
                .HasForeignKey(x => x.ExamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
