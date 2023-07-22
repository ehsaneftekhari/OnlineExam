using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Application.Mappers;
using OnlineExam.Application.Services;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IExamService, ExamService>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
        }
    }
}
