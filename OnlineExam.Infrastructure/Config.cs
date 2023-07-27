using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Infrastructure.Contract.IRepositories;
using OnlineExam.Infrastructure.Contract.IUnitOfWorks;
using OnlineExam.Infrastructure.Repositories;
using OnlineExam.Infrastructure.UnitOfWorks;

namespace OnlineExam.Infrastructure
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors, string ConnectionString)
        {
            serviceDescriptors.AddScoped<IExamRepository, ExamRepository>();
            serviceDescriptors.AddScoped<ITagRepository, TagRepository>();
            serviceDescriptors.AddScoped<ISectionRepository, SectionRepository>();
            serviceDescriptors.AddScoped<ITransactionUnitOfWork, TransactionUnitOfWork>();
            serviceDescriptors.AddDbContext<OnlineExamContext>(option => option.UseSqlServer(ConnectionString));
        }
    }
}
