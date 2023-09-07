using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    internal class AllowedFileTypesEntityTypeConfiguration : BaseModelEntityTypeConfiguration<AllowedFileTypesField>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<AllowedFileTypesField> builder)
        {
            builder.Property(x => x.Extension)
                .IsRequired()
                .HasColumnType(nameof(SqlDbType.NVarChar))
                .HasMaxLength(8);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType(nameof(SqlDbType.NVarChar))
                .HasMaxLength(512);
        }
    }
}
