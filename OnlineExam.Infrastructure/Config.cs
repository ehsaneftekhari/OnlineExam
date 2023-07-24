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
            serviceDescriptors.AddScoped<IExamHistoryRepository, ExamHistoryRepository>();
            serviceDescriptors.AddDbContext<OnlineExamContext>(option => option.UseSqlServer(ConnectionString));
        }
    }
}
