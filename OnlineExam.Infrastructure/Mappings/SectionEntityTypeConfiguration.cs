﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineExam.Infrastructure.Abstraction;
using OnlineExam.Model.Models;
using System.Data;

namespace OnlineExam.Infrastructure.Mappings
{
    public class SectionEntityTypeConfiguration : BaseModelEntityTypeConfiguration<Section>
    {
        public override void ConcreteConfigure(EntityTypeBuilder<Section> builder)
        {
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnType<string>(nameof(SqlDbType.NVarChar));

            builder.Property(p => p.Order)
               .IsRequired()
               .HasColumnType<int>(nameof(SqlDbType.Int))
               .HasDefaultValue(0);

            builder.Property(p => p.RandomizeQuestions)
               .IsRequired()
               .HasColumnType<bool>(nameof(SqlDbType.Bit))
               .HasDefaultValue(false);

            builder.HasOne(x => x.Exam)
                .WithMany(x => x.Sections)
                .HasForeignKey(x => x.ExamId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
