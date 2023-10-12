using OnlineExam.EndPoint.API.Middlewares;

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
            app.UseCors("AllowedLocalHostOrigin");
            //app.UseAuthorization();

            app.MapControllers();
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.Run();
        }
    }
}