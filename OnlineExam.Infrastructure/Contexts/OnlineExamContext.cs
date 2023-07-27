using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Mappings;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contexts
{
    public class OnlineExamContext : DbContext
    {
        public OnlineExamContext(DbContextOptions<OnlineExamContext> options) : base(options)
        {
        }

        public DbSet<Exam> Exam { get; set; }

        public DbSet<Section> Section { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ExamEntityTypeConfiguration())
                .ApplyConfiguration(new SectionEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
