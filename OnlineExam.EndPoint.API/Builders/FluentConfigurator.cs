using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Model.ConfigProviders;

namespace OnlineExam.EndPoint.API.Builders
{
    public class FluentConfigurator : IReaadyFluentConfigurator, IAppFluentConfigurator
    {
        IServiceCollection _ServiceCollection { get; set; }
        IConfiguration _Configuration { get; set; }
        List<(string name, CorsPolicy corsPolicy)> _CorsPolices { get; set; }
        WebApplication _WebApplication { get; set; }

        private FluentConfigurator(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            _ServiceCollection = serviceDescriptors;
            _Configuration = configuration;
            _CorsPolices = new List<(string name, CorsPolicy)>();
        }

        public static IReaadyFluentConfigurator Create(IServiceCollection serviceDescriptors, IConfiguration configuration)
        {
            return new FluentConfigurator(serviceDescriptors, configuration);
        }

        public IReaadyFluentConfigurator AddCors()
        {
            var CorsConfigurationsSections = _Configuration
                    .GetSection("CORSConfiguration")
                    .GetChildren();

            foreach (var section in CorsConfigurationsSections)
            {
                CorsPolicyBuilder policyBuilder = new CorsPolicyBuilder();

                var WithOriginsSection = section.GetSection("WithOrigins");
                if (WithOriginsSection.Value != "*")
                    policyBuilder.WithOrigins(WithOriginsSection.GetChildren().Select(x => x.Value).ToArray());
                else
                    policyBuilder.AllowAnyOrigin();

                var AllowAnyHeadersSection = section.GetSection("AllowAnyHeaders");
                if (AllowAnyHeadersSection.Value != "*")
                    policyBuilder.WithHeaders(AllowAnyHeadersSection.GetChildren().Select(x => x.Value).ToArray());
                else
                    policyBuilder.AllowAnyHeader();

                var AllowAnyMethodsSection = section.GetSection("AllowAnyMethods");
                if (AllowAnyMethodsSection.Value != "*")
                    policyBuilder.WithMethods(AllowAnyMethodsSection.GetChildren().Select(x => x.Value).ToArray());
                else
                    policyBuilder.AllowAnyMethod();

                _CorsPolices.Add((section.GetSection("Name").Value, policyBuilder.Build()));
            }

            _ServiceCollection.AddCors(options => _CorsPolices.ForEach(c => options.AddPolicy(c.name, c.corsPolicy)));

            return this;
        }

        public IReaadyFluentConfigurator AddMyIdentityConfiguration()
        {
            _ServiceCollection.AddSingleton(sp =>
            {
                var configurations = _Configuration.GetSection("MyIdentityConfiguration").GetChildren();
                return new IdentityConfiguration()
                {
                    TokenSigningKey = configurations.First(x => x.Key == "TokenSigningKey").Value,
                    TokenEncryptingKey = configurations.First(x => x.Key == "TokenEncryptingKey").Value,
                    ExpirationMinutes = TimeSpan.FromMinutes(int.Parse(configurations.First(x => x.Key == "ExpirationMinutes").Value)),
                    Audience = configurations.First(x => x.Key == "Audience").Value,
                };
            });

            return this;
        }

        public IReaadyFluentConfigurator AddApplication()
        {
            Application.Config.RegisterServices(_ServiceCollection);

            return this;
        }

        public IReaadyFluentConfigurator AddInfrastructure()
        {
            Infrastructure.Config.RegisterServices(_ServiceCollection, _Configuration.GetConnectionString("OnlineExamConnectionStrings"));
            _ServiceCollection.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<OnlineExamContext>();
            //.AddDefaultTokenProviders();
            //builder.Services.AddAuthorization();

            return this;
        }

        public IAppFluentConfigurator OnApp(WebApplication webApplication)
        {
            if(webApplication == null)
                throw new ArgumentNullException(nameof(webApplication));

            _WebApplication = webApplication;
            return this;
        }

        public IAppFluentConfigurator UseCors()
        {
            _CorsPolices.ForEach(c => _WebApplication.UseCors(c.name));
            return this;
        }
    }

    public interface IReaadyFluentConfigurator
    {
        IReaadyFluentConfigurator AddCors();
        IReaadyFluentConfigurator AddMyIdentityConfiguration();
        IReaadyFluentConfigurator AddApplication();
        IReaadyFluentConfigurator AddInfrastructure();
        IAppFluentConfigurator OnApp(WebApplication webApplication);
    }

    public interface IAppFluentConfigurator
    {
        IAppFluentConfigurator UseCors();
    }
}
