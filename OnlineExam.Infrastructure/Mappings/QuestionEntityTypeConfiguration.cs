using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    public class QuestionEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Question>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<Question> builder)
        {
            builder.HasOne(p => p.Section)
               .WithMany(s => s.Question)
               .HasForeignKey(x => x.SectionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.Text)
                .HasMaxLength(4000)
                .IsRequired()
                .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(p => p.ImageAddress)
               .HasMaxLength(256)
               .HasColumnType<string?>(nameof(SqlDbType.NVarChar));
        }
    }
}
