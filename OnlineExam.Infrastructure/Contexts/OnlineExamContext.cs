using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OnlineExam.Infrastructure.Mappings;
using OnlineExam.Infrastructure.SeedData;
using OnlineExam.Model.Models;

namespace OnlineExam.Infrastructure.Contexts
{
    public class OnlineExamContext : IdentityDbContext
    {
        public OnlineExamContext(DbContextOptions<OnlineExamContext> options) : base(options)
        {
        }

        public DbSet<Exam> Exam { get; set; }

        public DbSet<Section> Section { get; set; }

        public DbSet<Question> Question { get; set; }

        public DbSet<TextField> TextField { get; set; }

        public DbSet<CheckField> CheckField { get; set; }

        public DbSet<CheckFieldOption> CheckFieldOption { get; set; }

        public DbSet<FileField> FileField { get; set; }

        public DbSet<AllowedFileTypesField> AllowedFileTypes { get; set; }

        public DbSet<ExamUser> ExamUser { get; set; }

        public DbSet<Answer> Answer { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new ExamEntityTypeConfiguration())
                .ApplyConfiguration(new SectionEntityTypeConfiguration())
                .ApplyConfiguration(new QuestionEntityTypeConfiguration())
                .ApplyConfiguration(new TextFieldEntityTypeConfiguration())
                .ApplyConfiguration(new CheckFieldEntityTypeConfiguration())
                .ApplyConfiguration(new CheckFieldOptionEntityTypeConfiguration())
                .ApplyConfiguration(new FileFieldEntityTypeConfiguration())
                .ApplyConfiguration(new AllowedFileTypesEntityTypeConfiguration())
                .ApplyConfiguration(new ExamUserEntityTypeConfiguration())
                .ApplyConfiguration(new AnswerEntityTypeConfiguration());

            modelBuilder.Entity<IdentityRole>().HasData(IdentitySeedData.IdentityRoles);

            base.OnModelCreating(modelBuilder);
        }
    }
}
