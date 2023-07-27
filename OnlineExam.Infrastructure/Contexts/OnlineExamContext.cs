using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Mappings;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contexts
{
    public class OnlineExamContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public OnlineExamContext(DbContextOptions<OnlineExamContext> options) : base(options) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public DbSet<Exam> Exam { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ExamEntityTypeConfiguration())
                .ApplyConfiguration(new SectionEntityTypeConfiguration())
                .ApplyConfiguration(new TagEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
