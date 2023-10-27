using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OnlineExam.EndPoint.API.Builders;
using OnlineExam.EndPoint.API.Middlewares;
using OnlineExam.Infrastructure.Contexts;
using OnlineExam.Model.ConfigProviders;

namespace OnlineExam.EndPoint.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            // Add services to the container.

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var fluentConfigurator = FluentConfigurator.Create(builder.Services, configuration);

            fluentConfigurator.AddCors()
                .AddMyIdentityConfiguration()
                .AddApplication()
                .AddInfrastructure();

            var app = builder.Build();

            var appFluentConfigurator = fluentConfigurator.OnApp(app);

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            appFluentConfigurator.UseCors();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.Run();
        }
    }
}