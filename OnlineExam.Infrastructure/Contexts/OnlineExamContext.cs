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

        public DbSet<Question> Question { get; set; }

        public DbSet<TextField> TextField { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ExamEntityTypeConfiguration())
                .ApplyConfiguration(new SectionEntityTypeConfiguration())
                .ApplyConfiguration(new QuestionEntityTypeConfiguration())
                .ApplyConfiguration(new TextFieldEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
