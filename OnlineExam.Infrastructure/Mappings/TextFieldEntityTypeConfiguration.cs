using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class TextFieldEntityTypeConfiguration : IEntityTypeConfiguration<TextField>
    {
        public void Configure(EntityTypeBuilder<TextField> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
