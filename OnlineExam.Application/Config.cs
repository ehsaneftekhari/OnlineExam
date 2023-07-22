using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Contract.IServices;
using OnlineExam.Application.IMappers;
using OnlineExam.Application.Mappers;
using OnlineExam.Application.Services;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors, out AutofacServiceProviderFactory autoFacServiceProviderFactory)
        {
            autoFacServiceProviderFactory = new AutofacServiceProviderFactory
                (
                    x =>
                    {
                        x.RegisterType<ExamService>().As<IExamService>().InstancePerLifetimeScope();
                    }
                );

            //serviceDescriptors.AddScoped<IExamService, ExamService>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
        }
    }
}
