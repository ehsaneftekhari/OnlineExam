using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Infrastructure.Repositories;

namespace OnlineExam.Infrastructure
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors, string ConnectionString)
        {
            serviceDescriptors.AddScoped<IExamRepository, ExamRepository>();
            serviceDescriptors.AddScoped<ISectionRepository, SectionRepository>();
            serviceDescriptors.AddScoped<IQuestionRepository, QuestionRepository>();
            serviceDescriptors.AddScoped<ITextFieldRepository, TextFieldRepository>();
            serviceDescriptors.AddScoped<ICheckFieldRepository, CheckFieldRepository>();
            serviceDescriptors.AddScoped<ICheckFieldOptionRepository, CheckFieldOptionRepository>();
            serviceDescriptors.AddScoped<IFileFieldRepository, FileFieldRepository>();
            serviceDescriptors.AddScoped<IAllowedFileTypesFieldOptionRepository, AllowedFileTypesFieldOptionRepository>();
            serviceDescriptors.AddScoped<IExamUserRepository, ExamUserRepository>();
            serviceDescriptors.AddScoped<IAnswerRepository, AnswerRepository>();

            serviceDescriptors.AddDbContext<OnlineExamContext>(option => option.UseSqlServer(ConnectionString));
            //serviceDescriptors.AddIdentity<IdentityUser>()
            //    .AddEntityFrameworkStores<OnlineExamContext>()
            //    .AddUserStore<IdentityUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddRoleStore<IdentityRole>()
            //    .AddUserManager<UserManager<IdentityUser>>();

        }
    }
}
