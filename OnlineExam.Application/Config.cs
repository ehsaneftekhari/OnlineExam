using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using OnlineExam.Application.AttributeManagers;
using OnlineExam.Application.Contract.Markers;
using OnlineExam.Application.IMappers;
using OnlineExam.Application.Inteceptors;
using OnlineExam.Application.Mappers;

namespace OnlineExam.Application
{
    public class Config
    {
        public static void RegisterServices(IServiceCollection serviceDescriptors, out AutofacServiceProviderFactory autoFacServiceProviderFactory)
        {
            var reflectionHelper = new ReflectionHelper();
            var transactionUnitOfWorkAttributeManager = new TransactionUnitOfWorkAttributeManager(reflectionHelper);

            autoFacServiceProviderFactory = new AutofacServiceProviderFactory(
                    x =>
                    {
                        x.RegisterInstance(reflectionHelper).SingleInstance();
                        x.RegisterInstance(transactionUnitOfWorkAttributeManager).SingleInstance();
                        x.RegisterType<TransactionUnitOfWorkInterceptor>().InstancePerLifetimeScope();

                        var implementationsContract = reflectionHelper.GetImplementationContractInterfaces(typeof(IApplicationContractMarker));

                        foreach ((var implementation, var ContractInterface) in implementationsContract)
                        {
                            var registrationBuilder = x.RegisterType(implementation).As(ContractInterface).InstancePerLifetimeScope();

                            if (reflectionHelper.ClassHasAttribute(implementation, transactionUnitOfWorkAttributeManager.AttributeType))
                                registrationBuilder.EnableInterfaceInterceptors().InterceptedBy(typeof(TransactionUnitOfWorkInterceptor));
                        }
                    }
                );

            //serviceDescriptors.AddScoped<IExamService, ExamService>();
            serviceDescriptors.AddScoped<IExamMapper, ExamMapper>();
            serviceDescriptors.AddScoped<ISectionMapper, SectionMapper>();
            serviceDescriptors.AddScoped<ITagMapper, TagMapper>();
        }
    }
}
