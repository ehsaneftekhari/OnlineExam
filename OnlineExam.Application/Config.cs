using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.Contract.Markers;
using OnlineExam.Application.IMappers;
using OnlineExam.Application.Mappers;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors, out AutofacServiceProviderFactory autoFacServiceProviderFactory)
        {
            var registrationReflectionHelper = new ReflectionHelper();

            autoFacServiceProviderFactory = new AutofacServiceProviderFactory(
                    x =>
                    {
                        x.RegisterInstance(registrationReflectionHelper).SingleInstance();

                        var implementationsContract = registrationReflectionHelper.GetImplementationContractInterfaces(typeof(IApplicationContractMarker));

                        foreach ((var implementation, var ContractInterface) in implementationsContract)
                        {
                            var registrationBuilder = x.RegisterType(implementation)
                                                       .As(ContractInterface)
                                                       .InstancePerLifetimeScope();
                        }
                    }
                );

            serviceDescriptors.AddScoped<IExamHistoryMapper, ExamHistoryMapper>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
        }
    }
}
