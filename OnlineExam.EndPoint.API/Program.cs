using Microsoft.Extensions.Configuration;
using OnlineExam.EndPoint.API.Middlewares;
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
            builder.Services.AddSingleton(sp =>
            {
                var configuration = sp.GetRequiredService<IConfiguration>();
                return new IdentityConfiguration()
                {
                    Key = configuration
                        .GetSection("MyIdentityConfiguration")
                        .GetChildren()
                        .First(x => x.Key == "TokenKey").Value
                        
                };
            });

            Application.Config.RegisterServices(builder.Services);
            Infrastructure.Config.RegisterServices(builder.Services, builder.Configuration.GetConnectionString("OnlineExamConnectionStrings"));
            //builder.Services.AddAuthorization();


            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.Run();
        }
    }
}