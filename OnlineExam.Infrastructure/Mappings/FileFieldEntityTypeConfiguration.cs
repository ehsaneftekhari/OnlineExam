using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class FileFieldEntityTypeConfiguration : BaseModelEntityTypeConfiguration<FileField>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<FileField> builder)
        {
            builder.HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.AllowedFileTypes)
                .WithMany(x => x.FileFields);
        }
    }
}
