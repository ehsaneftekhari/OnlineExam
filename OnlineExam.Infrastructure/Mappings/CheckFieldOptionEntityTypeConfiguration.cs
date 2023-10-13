using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class CheckFieldOptionEntityTypeConfiguration : BaseModelEntityTypeConfiguration<CheckFieldOption>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<CheckFieldOption> builder)
        {
            builder.HasOne(x => x.CheckField)
                .WithMany()
                .HasForeignKey(x => x.CheckFieldId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.ImageAddress)
               .HasMaxLength(256)
               .HasColumnType<string?>(nameof(SqlDbType.NVarChar));

            builder.Property(x => x.Text)
                .HasMaxLength(4000)
                .HasColumnType<string?>(nameof(SqlDbType.NVarChar));
        }
    }
}
