using AutoMapper;
using DeviceMonitoringAPI.Interfaces;
using DeviceMonitoringAPI.Profiles;
using DeviceMonitoringAPI.Services;

namespace DeviceMonitoringAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:8080")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            builder.Services.AddAutoMapper(config =>
            {
                config.AddProfile<MappingProfile>();
            }, typeof(Program).Assembly);

            builder.Services.AddLogging();

            builder.Services.AddSingleton<ISessionStorageService, SessionStorageService>();
            builder.Services.AddScoped(typeof(ILoggerService<>), typeof(LoggerService<>));

            

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("AllowFrontend");

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
