using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class CheckFieldEntityTypeConfiguration : IEntityTypeConfiguration<CheckField>
    {
        public void Configure(EntityTypeBuilder<CheckField> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
