 using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowedLocalHostOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            // Add services to the container.
            builder.Services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                var configurations = configuration.GetSection("MyIdentityConfiguration").GetChildren();
                return new IdentityConfiguration()
                {
                    Key = configurations
                        .First(x => x.Key == "TokenKey").Value,

                    ExpirationMinutes = TimeSpan.FromMinutes(
                        int.Parse(configurations
                        .First(x => x.Key == "ExpirationTime").Value)),

                    Audience = configurations
                        .First(x => x.Key == "Audience").Value,
                };
            });

            Application.Config.RegisterServices(builder.Services);
            Infrastructure.Config.RegisterServices(builder.Services, builder.Configuration.GetConnectionString("OnlineExamConnectionStrings"));
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<OnlineExamContext>();
                //.AddDefaultTokenProviders();
            //builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("AllowedLocalHostOrigin");
            //app.UseAuthentication();
            //app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.Run();
        }
    }
}