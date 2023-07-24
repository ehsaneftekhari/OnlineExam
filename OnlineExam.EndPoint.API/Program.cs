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

            Application.Config.RegisterServices(builder.Services, out var autoFacServiceProviderFactory);
            builder.Host.UseServiceProviderFactory(autoFacServiceProviderFactory);

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

            app.Run();
        }
    }
}